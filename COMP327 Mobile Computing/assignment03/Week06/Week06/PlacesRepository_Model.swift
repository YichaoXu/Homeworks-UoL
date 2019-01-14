//
//  PlacesRepository.swift
//  Week06Particial
//
//  Created by aoo' Mac Mini on 27/11/2018.
//  Copyright © 2018 COMP327. All rights reserved.
//

import Foundation
import UIKit
import CoreData



class PlacesRepository{
    
    /* MARK:- Core Data*/
    private let context: NSManagedObjectContext
    
    /* The number of the places managed by the core data */
    var count: Int {
        let request = NSFetchRequest<NSFetchRequestResult>(entityName: "Place")
        request.resultType = .countResultType
        request.returnsObjectsAsFaults = false
        do{
            return try context.fetch(request)[0] as! Int 
        }catch{
            return 0
        }
    }
    
    /* MARK:- The repository sigleton */
    private static var repositorySingleton: PlacesRepository?
    
    public static func getRepositoryPointer() -> PlacesRepository{
        if PlacesRepository.repositorySingleton == nil {
            PlacesRepository.repositorySingleton = PlacesRepository()
        }
        return PlacesRepository.repositorySingleton!
    }
    
    private init() {
        let appDelegate = UIApplication.shared.delegate as! AppDelegate
        context = appDelegate.persistentContainer.viewContext
    }
    
    /* MARK:- Public interface */
    public func getPlace(by id: Int16) -> Place? {
        let request = NSFetchRequest<NSFetchRequestResult>(entityName: "Place")
        request.predicate = NSPredicate(format: "id == %@", "\(id)")
        request.returnsObjectsAsFaults = false
        do{
            return try context.fetch(request)[0] as? Place
        }catch{
            print("ReportRespoitory.requireReports(Int): Request Error")
            return nil
        }
    }
    
    public func getAllNearbyPlace() -> [Place] {
        let request = NSFetchRequest<NSFetchRequestResult>(entityName: "Place")
        request.returnsObjectsAsFaults = false
        do{
            return try context.fetch(request) as! [Place]
        }catch{
            print("ReportRespoitory.requireReports(Int): Request Error")
            return [Place]()
        }
    }
    
    @discardableResult
    public func store(name:String, lat:Double, lon: Double) -> Place?{
        let newPlace = NSEntityDescription.insertNewObject(forEntityName: "Place", into: context) as! Place
        newPlace.id = Int16(count)
        newPlace.name = name
        newPlace.latitude = lat
        newPlace.longitude = lon
        do {
            try context.save()
            return newPlace
        } catch {
            print("PlacesRepository.store(_ place: Place): cannot obtain saved data")
            return nil
        }
    }
}
