//
//  FavourStoreModel.swift
//  assignment01
//
//  Created by aoo' Mac Mini on 10/11/2018.
//  Copyright © 2018 COMP327. All rights reserved.
//

import Foundation
import CoreData

// MARK: - Structure
/* The structure is used to cache last request */
struct ReportsCache {
    var year: Int
    var cache:[Report]
}

// MARK: - Extension
/* Extension of the Report: ManagedObject*/
extension Report{
    func setValues(from rawReportData: RawReportData) {
        self.year = rawReportData.year
        self.id = rawReportData.id
        self.owner = rawReportData.owner ?? ""
        self.authors = rawReportData.authors
        self.title = rawReportData.title
        self.abstract = rawReportData.abstract ?? ""
        self.pdf = rawReportData.pdf?.absoluteString ?? ""
        self.comment = rawReportData.comment ?? ""
        self.lastModified = rawReportData.lastModified
        self.isFavour = false
    }
}


// MARK: - Class
/* The class which is used to manage the reports objects from data-sources*/
class ReportRespoitory{
    /* The local data path is used to store the collected data-sources' id */
    private let dataSourcesLogs = "/dataSourceLogs.plist"
    private let docuementPath = NSSearchPathForDirectoriesInDomains(.documentDirectory, .userDomainMask, true)[0]
    
    /* The core data ManagedObjectContext */
    private let context: NSManagedObjectContext
    
    /* The range of years for all reports in the respoitory */
    private(set) var yearRange: [Int]
    /* The cache data */
    private var reportsCache: ReportsCache
    
    init(in coreDataContext: NSManagedObjectContext) {
        context = coreDataContext
        yearRange = [Int]()
        reportsCache = ReportsCache(year: 0, cache: [Report]())
    }
    
    /* The funvtion is used to collect the reports from datasource. */
    func collectReports(from source: ReportDataSource){
        guard !isCollected(from: source) else{ return }
        for anyRawReportData in source.rawReportDataList{
            let newReport = NSEntityDescription.insertNewObject(forEntityName: "Report", into: context) as! Report
            newReport.setValues(from: anyRawReportData)
        }
        do {
            try context.save()
            print(docuementPath+dataSourcesLogs)
            var tmpArr = NSArray(contentsOfFile: docuementPath+dataSourcesLogs) ?? NSArray()
            tmpArr = tmpArr.adding(source.identification) as NSArray
            tmpArr.write(toFile:docuementPath+dataSourcesLogs, atomically: true)
        } catch {
            print("EventModel.init(NSManagedObjectContext, ReportDataSource): cannot obtain saved data")
        }
    }
    
    /* The function is used to check if the data-source has been collected*/
    func isCollected(from source: ReportDataSource) -> Bool {
        let tmpArr = NSArray(contentsOfFile: docuementPath + dataSourcesLogs)
        return tmpArr?.contains(source.identification) ?? false
    }
    
    /* The function is used to obtain the year range of the reports */
    func getYearRange()->[Int]{
        guard yearRange.isEmpty else{return yearRange}
        let request = NSFetchRequest<NSFetchRequestResult>(entityName: "Report")
        request.propertiesToFetch = ["year"]
        request.returnsDistinctResults = true
        request.resultType = .dictionaryResultType
        do{
            let tmpDictionaty = try context.fetch(request) as! [[String: String]]
            for rst in tmpDictionaty{
                yearRange.append(Int(rst["year"]!)!)
            }
            yearRange.sort()
        }catch{
            print("ReportRespoitory.getYearRange(): cannot get the years")
        }
        return yearRange
    }
    
    /* The function is used to require all reports in specified year */
    func requireReports(In year: Int) -> [Report] {
        guard yearRange.contains(year) else { return [Report]() }
        if year == reportsCache.year {return reportsCache.cache}
        let request = NSFetchRequest<NSFetchRequestResult>(entityName: "Report")
        request.predicate = NSPredicate(format: "year == %@", "\(year)")
        request.returnsObjectsAsFaults = false
        do{
            let tmpArr = try context.fetch(request) as! [Report]
            reportsCache = ReportsCache(year: year, cache: tmpArr)
            return tmpArr
        }catch{
            print("ReportRespoitory.requireReports(Int): Request Error")
            return [Report]()
        }
    }
    
    /* The function is used to require the number reports in specified year */
    func requireNumberOfReports(In year: Int) -> Int {
        guard yearRange.contains(year) else { return 0 }
        let request = NSFetchRequest<NSFetchRequestResult>(entityName: "Report")
        request.predicate = NSPredicate(format: "year == %@", "\(year)")
        request.sortDescriptors = [NSSortDescriptor(key: "id", ascending: true)]
        request.resultType = .countResultType
        request.returnsObjectsAsFaults = false
        do{
            let tmpRst = try context.fetch(request)[0]
            return tmpRst as! Int
        }catch{
            print("ReportRespoitory.requireNumberOfReports(Int): cannot get the number of years")
            return 0
        }
    }
    
    /* The function is used to change the favourite state of a report */
    func changeFavourite(report:Report){
        let request = NSFetchRequest<NSFetchRequestResult>(entityName: "Report")
        request.predicate = NSPredicate(format: "year == %@ AND id == %@", "\(report.year!)", "\(report.id!)")
        request.resultType = .managedObjectResultType
        request.returnsObjectsAsFaults = false
        do{
            let tmpRst = try context.fetch(request)[0] as! Report
            tmpRst.isFavour = !(tmpRst.isFavour)
            try context.save()
        }catch{
            print("ReportRespoitory.changeFavourite(Report): Cannot change the isFavour")
        }
    }
}

