//
//  PlacesRepositoryModel.swift
//  assignment02
//
//  Created by aoo' Mac Mini on 30/11/2018.
//  Copyright © 2018 COMP327. All rights reserved.
//

import Foundation
import CoreLocation

/*
 The class is the model of artworks and locations manager.
 It implements the singleton design pattern.
 */
class RepositoriesManager{
    /* singleton */
    static let shared = RepositoriesManager()
    
    // MARK:- Varables
    private var searchingkeyWord: String
    private var dataSource: WebDataSource?
    private let artworkRepository: Repository<Artwork>
    private let locationRepository: Repository<Location>
    private var locationArray: [Location]?
    private var currentCoordinate: CLLocation
    
    // MARK:- Public Interface
    /* setting the datasource for all repositories */
    func setDataSource(to source: WebDataSource){
        dataSource = source
    }
    
    /* clear all data stored in all repositories */
    func clearRepositories(){
        artworkRepository.clear()
        locationRepository.clear()
    }
    
    /* Updates all new data in the website */
    func updateRepositories() {
        if SystemSetting.shared.getSetting(by: "clearing local data each time")! {
            RepositoriesManager.shared.clearRepositories()
            print("RepositoriesManager.updateRepositories(): previous data have been cleared")
        }
        while let newData = dataSource?.next(){
            let namePredicate = NSPredicate(format: "name = %@", newData.location)
            locationRepository.updateData(with: namePredicate, by: newData)
            let titlePredicate = NSPredicate(format: "title = %@", newData.title)
            artworkRepository.updateData(with: titlePredicate, by: newData)
        }
        print("RepositoriesManager.updateRepositories(): new data have been stored")
        locationArray = locationRepository.requestEntities(with: nil, sortedBy: nil)
    }
    
    /* get all locations stored in locationRespository */
    func getAllLocations() -> [Location] {
        return locationArray!
    }
    
    /* get all artworks of a location stored in artworksRespository */
    func getArtworks(at location: Location) -> [Artwork] {
        let predicate = NSPredicate(format: "locationName = %@",location.name!)
        let rstArray = artworkRepository.requestEntities(with: predicate, sortedBy: "title") ?? [Artwork]()
        if searchingkeyWord.isEmpty {
            return rstArray
        } else if SystemSetting.shared.getSetting(by: "searching includes building name")!
            && location.name!.containsCaseless(searchingkeyWord) {
            return rstArray
        } else if SystemSetting.shared.getSetting(by: "using fuzzy searching")!{
            let regexStr = searchingkeyWord.formateForFuzzySearching()
            let titlePredicate = NSPredicate(format: "SELF MATCHES %@", regexStr)
            return rstArray.filter{ return titlePredicate.evaluate(with: $0.title?.lowercased()) }
        } else {
            let titlePredicate = NSPredicate(format: "SELF CONTAINS %@", searchingkeyWord)
            return rstArray.filter{ return titlePredicate.evaluate(with: $0.title) }
        }
        
    }
    
    /* Update user location to new location */
    func updateDeviceLocation(by coordinate: CLLocation){
        let distant = coordinate.distance(from: currentCoordinate)
        guard distant > 0.1 else { return }
        currentCoordinate = coordinate
        sortLocationsByDistance()
    }
    
    /* Update searching keyword */
    func setSearchingKeyword(to newKeyWord: String){
        searchingkeyWord = newKeyWord
    }
    
    // MARK:- Private Methods
    private init() {
        searchingkeyWord = ""
        artworkRepository = Repository<Artwork>()
        locationRepository = Repository<Location>()
        currentCoordinate = CLLocation(latitude: 53.406561, longitude: -2.966203)
    }
    
    private func sortLocationsByDistance() {
        locationArray!.sort() {
            let firstCooridate = CLLocation(latitude: $0.latitude, longitude: $0.longitude)
            let firstDistance = firstCooridate.distance(from: currentCoordinate)
            let secondCooridate = CLLocation(latitude: $1.latitude, longitude: $1.longitude)
            let secondDistance = secondCooridate.distance(from: currentCoordinate)
            return firstDistance < secondDistance
        }
    }
}

extension String{
    func containsCaseless(_ other: String) -> Bool{
        return self.lowercased().contains(other.lowercased())
    }
    
    func formateForFuzzySearching()->String{
        let tmpStr = self.lowercased()
        var result = "^.*"
        for eachCharacter in tmpStr {
            result.append(eachCharacter)
            result.append(".*")
        }
        result.append("$")
        return result
    }
}
