//
//  CoreDataModel.swift
//  assignment02
//
//  Created by aoo' Mac Mini on 02/12/2018.
//  Copyright © 2018 COMP327. All rights reserved.
//

import Foundation
import UIKit
import CoreData

/* The class is the model of coredata. */
class Repository<T: Transferable> {
    
    private var context: NSManagedObjectContext
    private var cache: NSCache<NSString, NSArray>
    
    init() {
        let appDelegate = UIApplication.shared.delegate as! AppDelegate
        context = appDelegate.persistentContainer.viewContext
        cache = NSCache<NSString, NSArray>()
        cache.totalCostLimit = 50 * 1024 * 1024
    }
    
    /* Request entities that satisfy a predicate */
    func requestEntities(with predicate: NSPredicate?, sortedBy sortKey: String?) -> [T]? {
        let cacheKey = getCacheKeyBy(predicate, and: sortKey)
        if let cacheData = cache.object(forKey: cacheKey) { return cacheData as? [T] }
        let tmpRst = requestFromCoreData(with: predicate, sortedBy: sortKey)
        if tmpRst != nil { cache.setObject(tmpRst! as NSArray, forKey: cacheKey) }
        return tmpRst as? [T]
    }
    
    @discardableResult
    func updateData(with predicate: NSPredicate, by newRawData: RawData) -> Bool {
        guard let tmpRst = requestFromCoreData(with: predicate, sortedBy: nil) else { return false }
        if tmpRst.count != 0 { // if there is already an object satisfying the predicate
            // Then, changes the object's attributes
            let previousData = tmpRst[0] as! T
            // guard the object stored in the core date is not same with new one
            guard !previousData.isSame(contentOf: newRawData) else { return false }
            previousData.setValue(by: newRawData)
        } else {
            // create a new object
            let newEntity = NSEntityDescription.insertNewObject(forEntityName: T.typeName, into: context) as! Transferable
            newEntity.setValue(by: newRawData)
            cache.removeAllObjects()
        }
        do {
            try context.save()
            return true
        } catch {
            print("Repository Name: \(T.typeName)\n" +
                "Repository.updateData(with:to:) :\n", error
            )
            return false
        }
    }
    /* Clear all data store in the respository */
    func clear(){
        cache.removeAllObjects()
        let deleteFetch = NSFetchRequest<NSFetchRequestResult>(entityName: T.typeName)
        let deleteRequest = NSBatchDeleteRequest(fetchRequest: deleteFetch)
        do {
            try context.execute(deleteRequest)
            try context.save()
        } catch {
            print ("Repository.clear(): Fail")
        }
    }
    
    private func requestFromCoreData(with predicate: NSPredicate?, sortedBy sortKey: String?) -> [Any]? {
        let request = NSFetchRequest<NSFetchRequestResult>(entityName: T.typeName)
        request.resultType = .managedObjectResultType
        request.predicate = predicate
        request.returnsObjectsAsFaults = false
        if sortKey != nil { request.sortDescriptors = [NSSortDescriptor(key: sortKey, ascending: true)] }
        do{
            return try context.fetch(request)
        } catch {
            print("Repository Name: \(T.typeName)\nRequest Predicate:", predicate?.predicateFormat ?? "" , "\nRepository.requestEntities(with:) :", error
            )
            return nil
        }
    }
    
    private func getCacheKeyBy(_ predicate: NSPredicate?, and sortedKey:String?) -> NSString{
        let predicateStr = predicate?.predicateFormat ?? "all entity"
        let sortedKeyStr =  sortedKey ?? "none"
        let key = "requests \(predicateStr) which is sorted by \(sortedKeyStr)"
        return key as NSString
    }
    
    
}
