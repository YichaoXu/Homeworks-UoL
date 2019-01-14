//
//  TableViewController.swift
//  assignment02
//
//  Created by aoo' Mac Mini on 30/11/2018.
//  Copyright © 2018 COMP327. All rights reserved.
//

import UIKit
import MapKit

/* The class is the controller of the main page */
class ArtsMapPageController: UIViewController {
    
    @IBOutlet weak var searchBar: UISearchBar!
    @IBOutlet weak var table: UITableView!
    @IBOutlet weak var pageBorder: NSLayoutConstraint!
    @IBOutlet weak var mapView: MKMapView!
    private var locationManager:CLLocationManager!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        // Firstly, check update from the website
        if let webSource = DownLoader.shared.downloadWebsite(){
            RepositoriesManager.shared.setDataSource(to: webSource)
        } else {
            print("viewDidLoad(): Cannot load data.")
        }
        RepositoriesManager.shared.updateRepositories()
        
        //Secondly, initiate the gps
        locationManager = CLLocationManager()
        locationManager.delegate = self as CLLocationManagerDelegate
        locationManager.desiredAccuracy = kCLLocationAccuracyBestForNavigation
        locationManager.requestWhenInUseAuthorization()
        locationManager.startUpdatingLocation()
        
        // Finally, show all annotaions on map
        for eachLocation in RepositoriesManager.shared.getAllLocations() {
            let annotation = MKPointAnnotation()
            let coordinate = CLLocationCoordinate2D(
                latitude: eachLocation.latitude,
                longitude: eachLocation.longitude
            )
            annotation.coordinate = coordinate
            annotation.title = eachLocation.name
            mapView.addAnnotation(annotation)
        }
    }
    
    override func viewWillAppear(_ animated: Bool) {
        // Initiate an observer for the keyboard
        NotificationCenter.default.addObserver(
            self,
            selector: #selector(keyboardWillShow),
            name: UIResponder.keyboardWillShowNotification,
            object: nil
        )
        NotificationCenter.default.addObserver(
            self,
            selector: #selector(keyboardWillHide),
            name: UIResponder.keyboardWillHideNotification,
            object: nil
        )
        table.reloadData()
    }
    
    override func viewWillDisappear(_ animated: Bool) {
        // Remove all observer
        NotificationCenter.default.removeObserver(self)
    }

    /* Selector method for the keyboardWillShow event */
    @objc private func keyboardWillShow(_ notification: Notification) {
        if let keyboardFrame: NSValue = notification.userInfo?[UIResponder.keyboardFrameEndUserInfoKey] as? NSValue {
            let keyboardRectangle = keyboardFrame.cgRectValue
            let animationDuration : TimeInterval = 0.30
            UIView.beginAnimations("ResizeForKeyboard", context: nil)
            UIView.setAnimationDuration(animationDuration)
            self.pageBorder.constant = keyboardRectangle.height
        }
    }
    
    /* Selector method for the keyboardWillHide event */
    @objc private func keyboardWillHide() {
        let animationDuration : TimeInterval = 0.30
        UIView.beginAnimations("ResizeForKeyboard", context: nil)
        UIView.setAnimationDuration(animationDuration)
        self.pageBorder.constant = CGFloat(0)
    }
}
