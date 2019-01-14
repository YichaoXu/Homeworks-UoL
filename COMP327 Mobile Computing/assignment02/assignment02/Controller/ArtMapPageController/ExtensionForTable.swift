//
//  ArtPageController_ExtensionForTable.swift
//  assignment02
//
//  Created by aoo' Mac Mini on 11/12/2018.
//  Copyright © 2018 COMP327. All rights reserved.
//

import UIKit

/* Extension of the UITableViewDataSource and UITableViewDelegate */
extension ArtsMapPageController:  UITableViewDataSource{
    
    func numberOfSections(in tableView: UITableView) -> Int {
        return RepositoriesManager.shared.getAllLocations().count
    }
    
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        let tmpLocation = RepositoriesManager.shared.getAllLocations()[section]
        return RepositoriesManager.shared.getArtworks(at: tmpLocation).count
    }
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let cell = UITableViewCell(style: UITableViewCell.CellStyle.subtitle, reuseIdentifier: "cell")
        let tmpLocation = RepositoriesManager.shared.getAllLocations()[indexPath.section]
        let tmpArtwork = RepositoriesManager.shared.getArtworks(at: tmpLocation)[indexPath.row]
        if tmpArtwork.isEnable {
            cell.textLabel?.text = tmpArtwork.title!
        } else {
            cell.backgroundColor = UIColor.gray
            cell.textLabel?.text = "\(tmpArtwork.title!) currently unenable"
        }
        cell.detailTextLabel?.text = tmpArtwork.artist
        return cell
    }
    
    func tableView(_ tableView: UITableView, viewForHeaderInSection section: Int) -> UIView? {
        let tmpLocation = RepositoriesManager.shared.getAllLocations()[section]
        let headerView = UITableViewHeaderFooterView()
        headerView.textLabel?.text = tmpLocation.name
        return headerView
    }
}

extension ArtsMapPageController: UITableViewDelegate{
    
    func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
        tableView.deselectRow(at: indexPath, animated: true)
        let tmpLocaiton = RepositoriesManager.shared.getAllLocations()[indexPath.section]
        let tmpArtwork = RepositoriesManager.shared.getArtworks(at: tmpLocaiton)[indexPath.row]
        self.performSegue(withIdentifier: "toDetailPage", sender: tmpArtwork)
    }
    
    override func prepare(for segue: UIStoryboardSegue, sender: Any?) {
        if segue.identifier == "toDetailPage"{
            let detailPageController = segue.destination as! DetailPageViewController
            guard let tmpArtwork = sender as? Artwork else { return }
            detailPageController.artwork = tmpArtwork
        }
    }
}
