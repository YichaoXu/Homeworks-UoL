//
//  ViewController.swift
//  Week06Particial
//
//  Created by aoo' Mac Mini on 26/11/2018.
//  Copyright © 2018 COMP327. All rights reserved.
//

import UIKit
import MapKit

class MapViewController: UIViewController, MKMapViewDelegate {
    
    var placeID: Int16?
    var isClickedAddButton: Bool = true
    
    private var placesRepository: PlacesRepository!
    
    private static let DEFAULT_CENTRE = CLLocationCoordinate2D(latitude: 53.406, longitude: -2.967)
    
    @IBOutlet weak var map: MKMapView!
    
    
    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view, typically from a nib.
        placesRepository = PlacesRepository.getRepositoryPointer()
        if isClickedAddButton {
            //initiateAddView
            changeCentreCoordinate(to: MapViewController.DEFAULT_CENTRE)
            let nearbyPlaces = placesRepository.getAllNearbyPlace()
            for eachPlace in nearbyPlaces{
                mark(eachPlace)
            }
            let uilpgr = UILongPressGestureRecognizer(target: self, action: .longPressScreen)
            uilpgr.minimumPressDuration = 2
            map.addGestureRecognizer(uilpgr)
        }else {
            //initiateCellView
            guard let id = placeID, let place = placesRepository.getPlace(by: id) else {return}
            changeCentreCoordinate(to: place.coordinate)
            mark(place)
        }
    }
    
    private func changeCentreCoordinate(to coordinate: CLLocationCoordinate2D) {
        let span = MKCoordinateSpan(latitudeDelta: 0.008, longitudeDelta: 0.008)
        let region = MKCoordinateRegion(center: coordinate, span: span)
        self.map.setRegion(region, animated: true)
    }

    private func mark(_ place: Place){
        let annotation = MKPointAnnotation()
        annotation.coordinate = place.coordinate
        annotation.title = place.name
        self.map.addAnnotation(annotation)
    }
    
    @objc func longpress(gestureRecognizer: UIGestureRecognizer) {
        guard gestureRecognizer.state == UIGestureRecognizer.State.began else{ return }
        let touchPoint = gestureRecognizer.location(in: self.map)
        let newCoordinate = self.map.convert(touchPoint, toCoordinateFrom: self.map)
        let location = CLLocation(latitude: newCoordinate.latitude, longitude: newCoordinate.longitude)
        CLGeocoder().reverseGeocodeLocation(location, completionHandler: { (placeMarks, error) in
            var title = ""
            if error != nil {
                print(error!)
            } else if let placeMark = placeMarks?[0] {
                let thoroughfare = placeMark.thoroughfare
                let subThoroughfare = placeMark.subThoroughfare
                title = [thoroughfare, subThoroughfare].compactMap{$0}.joined(separator: " ")
            }
            if title == "" {title = "Added \(NSDate())"}
            let newPlace = self.placesRepository.store(name: title, lat: newCoordinate.latitude, lon: newCoordinate.longitude)
            self.mark(newPlace!)
        })
    }
}

extension Selector{
    static let longPressScreen = #selector(MapViewController.longpress(gestureRecognizer:))
}

extension Place{
    var coordinate:  CLLocationCoordinate2D {
        return CLLocationCoordinate2D(latitude: self.latitude, longitude: self.longitude)
    }
}

