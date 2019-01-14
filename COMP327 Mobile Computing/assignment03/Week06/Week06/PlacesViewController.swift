//
//  PlacesViewController.swift
//  Week06Particial
//
//  Created by aoo' Mac Mini on 26/11/2018.
//  Copyright © 2018 COMP327. All rights reserved.
//

import UIKit

class PlacesViewController: UITableViewController {

    @IBOutlet var table: UITableView!
    private var placesRepository: PlacesRepository!

    
    override func viewDidLoad() {
        placesRepository = PlacesRepository.getRepositoryPointer()
        if placesRepository.count == 0{
            placesRepository.store(name:"Ashton Building", lat: 53.406566, lon: -2.966531)
        }
    }

    override func viewWillAppear(_ animated: Bool){
        let start = table.numberOfRows(inSection: 0)
        let end = placesRepository.count - 1
        guard start <= end else {return}
        var newRows = [IndexPath]()
        for eachNewRow in start...end{
            newRows.append(IndexPath(row: eachNewRow, section: 0))
        }
        table.insertRows(at: newRows, with: .fade)
    }
    
    // MARK: - Table view data source
    override func numberOfSections(in tableView: UITableView) -> Int {
        return 1
    }

    override func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return placesRepository.count
    }
    
    
    override func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let cell = UITableViewCell(style: UITableViewCell.CellStyle.default, reuseIdentifier: "cell")
        if let place = placesRepository.getPlace(by: Int16(indexPath.row+1)){
            cell.textLabel?.text = place.name
        }
        return cell
    }
    
    override func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
        tableView.deselectRow(at: indexPath, animated: true)
        let parameters = ["id":indexPath.row+1, "isClickedAddButton": false] as [String : Any]
        self.performSegue(withIdentifier: "toMap", sender: parameters)
    }
    
    override func prepare(for segue: UIStoryboardSegue, sender: Any?) {
        if segue.identifier == "toMap"{
            let mapController = segue.destination as! MapViewController
            if let parameters = sender as? [String : Any]{
                mapController.placeID = parameters["id"] as? Int16
                mapController.isClickedAddButton = (parameters["isClickedAddButton"] as! Bool)
            }
        }
    }
}
