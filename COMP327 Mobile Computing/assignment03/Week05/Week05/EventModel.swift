//
//  EventModel.swift
//  Week05
//
//  Created by aoo' Mac Mini on 10/11/2018.
//  Copyright © 2018 COMP327. All rights reserved.
//

import Foundation
import CoreData

class EventModel{
    private let context: NSManagedObjectContext
    private(set) var uolEventArr: [UolEvent]
    
    init(AppleDelegate delegate: AppDelegate) {
        context = delegate.persistentContainer.viewContext
        do {
            let request = NSFetchRequest<NSFetchRequestResult>(entityName: "UolEvent")
            request.predicate = NSPredicate(format: "place <> %@", "")
            request.returnsObjectsAsFaults = false
            uolEventArr = try context.fetch(request) as! [UolEvent]
        } catch {
            print("EventModel.init(AppleDelegate): cannot obtain coredata")
            uolEventArr = [UolEvent]()
        }
    }
    
    func addUolEvent(with place: String) {
        do {
            let newUolEvent = NSEntityDescription.insertNewObject(forEntityName: "UolEvent", into: context) as! UolEvent
            newUolEvent.place = place
            try context.save()
            uolEventArr.append(newUolEvent)
        } catch {
            print("addUolEvent(String): cannot save into coredata")
        }
    }
}
