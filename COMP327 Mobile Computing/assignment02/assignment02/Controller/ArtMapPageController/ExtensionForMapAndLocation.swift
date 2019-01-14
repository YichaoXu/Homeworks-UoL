//
//  ExtensionForMap.swift
//  assignment02
//
//  Created by aoo' Mac Mini on 11/12/2018.
//  Copyright © 2018 COMP327. All rights reserved.
//

import MapKit

/* Implementation of the MKMapViewDelegate and CLLocationManagerDelegate */
extension ArtsMapPageController: MKMapViewDelegate{
    
    func mapView(_ mapView: MKMapView, didSelect view: MKAnnotationView) {
        
        // Identify the clicked annotation
        guard let locationName = view.annotation?.title else { return }
        var clickedLocation: Location?
        for anyone in RepositoriesManager.shared.getAllLocations() {
            if anyone.name == locationName {
                clickedLocation = anyone
                break
            }
        }
        guard clickedLocation != nil else { return }
        
        // show an alert to list the title and image for all artworks in the location.
        let alert = UIAlertController(title: clickedLocation?.name, message: nil, preferredStyle: .alert)
        for eachArtwork in RepositoriesManager.shared.getArtworks(at: clickedLocation!) {
            guard eachArtwork.isEnable else { continue }// guard the artwork is enable else ignore the artwork
            var tmpTitle = eachArtwork.title!
            if tmpTitle.count > 12 { // if the title length is to long
                tmpTitle = String(tmpTitle.prefix(12)) + "..."
            }
            let action = UIAlertAction(title: tmpTitle, style: .default) { (ignore) in
                self.performSegue(withIdentifier: "toDetailPage", sender: eachArtwork)
            }
            if let eachImgName = eachArtwork.imgFileName,
                let tmpImgData = DownLoader.shared.downloadImageAndCache(with: eachImgName) {
                let img = UIImage(data: tmpImgData)!
                let reSizedImg = img.reSizeImage(CGSize(width: 50, height: 50))
                let originalImg = reSizedImg.withRenderingMode(.alwaysOriginal)
                action.setValue(originalImg, forKey: "image")
            }
            alert.addAction(action)
        }
        let cancel = UIAlertAction(title: "cancel", style: .cancel, handler: nil)
        alert.addAction(cancel)
        self.present(alert, animated: true, completion: nil)
    }
}

extension ArtsMapPageController:  CLLocationManagerDelegate {
    
    func locationManager(_ manager: CLLocationManager, didUpdateLocations locations: [CLLocation]) {
        let locationOfUser = locations[0]
        RepositoriesManager.shared.updateDeviceLocation(by: locationOfUser)
        let span = MKCoordinateSpan(latitudeDelta: 0.002, longitudeDelta: 0.002)
        let region = MKCoordinateRegion(center: locationOfUser.coordinate, span: span)
        self.mapView.setRegion(region, animated: true)
        self.table.reloadData()
    }
    
}

extension UIImage {

    func reSizeImage(_ reSize:CGSize) -> UIImage {
        UIGraphicsBeginImageContextWithOptions(reSize,false,UIScreen.main.scale)
        self.draw(in: CGRect(x: 0, y: 0, width: reSize.width, height: reSize.height))
        let reSizeImage = UIGraphicsGetImageFromCurrentImageContext()
        UIGraphicsEndImageContext()
        return reSizeImage!
    }
}
