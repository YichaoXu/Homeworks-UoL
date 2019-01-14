//
//  ExtensionForSearchBar.swift
//  assignment02
//
//  Created by aoo' Mac Mini on 11/12/2018.
//  Copyright © 2018 COMP327. All rights reserved.
//

import UIKit

/* Implementation of the UISearchBarDelegate */
extension ArtsMapPageController: UISearchBarDelegate {
    
    func searchBarSearchButtonClicked(_ searchBar: UISearchBar) {
        searchBar.resignFirstResponder()
    }
    
    func searchBar(_ searchBar: UISearchBar, textDidChange searchText: String) {
        RepositoriesManager.shared.setSearchingKeyword(to: searchText)// Set keyword for model
        self.table.reloadData()
    }
}
