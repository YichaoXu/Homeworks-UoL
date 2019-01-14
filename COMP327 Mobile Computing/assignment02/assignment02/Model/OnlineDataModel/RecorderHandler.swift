//
//  RecorderHandler.swift
//  assignment02
//
//  Created by aoo' Mac Mini on 11/12/2018.
//  Copyright © 2018 COMP327. All rights reserved.
//

import Foundation

class LocalPlist<T: Collection>{
    
    private let localFilePath: String
    private var localData: T
    
    init?(with fileName: String){
        localFilePath = NSHomeDirectory() + "/Documents/" + fileName
        guard let tmpArray = NSArray(contentsOfFile: localFilePath) as? T else { return nil }
        localData = tmpArray
    }
    
    deinit {
        
    }
    
    func transferRecordsToWebSources() -> [WebDataSource] {
        var records = [WebDataSource]()
        for eachRecordData in localRecordsDatasList {
            guard let tmpRecord = WebSourceRecord(from: eachRecordData) else { continue }
            let tmpDataSource = WebDataSource(from: tmpRecord)
            records.append(tmpDataSource)
        }
        return records
    }
    
    func records(_ newSource: WebDataSource){
        let webRecord = newSource.getWebSourceRecord()
        localRecordsDatasList.append(webRecord.toDictionary())
        (localRecordsDatasList as NSArray).write(toFile: localFilePath, atomically: true)
    }
}

