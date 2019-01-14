//
//  TransferableProtocol.swift
//  assignment02
//
//  Created by aoo' Mac Mini on 03/12/2018.
//  Copyright © 2018 COMP327. All rights reserved.
//

import Foundation

/* The protocol describes the class which can be transferred to ManagedObject */
protocol Transferable {
    static var typeName: String {get}
    func setValue(by rawData: RawData)
    func isSame(contentOf rawData: RawData) -> Bool
}

extension Artwork: Transferable{
    
    static let typeName = "Artwork"
    
    func setValue(by rawData: RawData) {
        id = Int16(rawData.id)!
        title = rawData.title
        artist = rawData.artist
        yearOfWork = Int16(rawData.yearOfWork)!
        information = rawData.Information
        locationName = rawData.location
        imgFileName = rawData.fileName
        isEnable = (rawData.enabled == "1")
    }
    
    func isSame(contentOf rawData: RawData) -> Bool {
        return id == Int16(rawData.id)!
            && title == rawData.title
            && artist == rawData.artist
            && yearOfWork == Int16(rawData.yearOfWork)!
            && information == rawData.Information
            && locationName == rawData.location
            && imgFileName == rawData.fileName
            && isEnable == (rawData.enabled == "1")
    }
}

extension Location: Transferable{
    
    static let typeName = "Location"
    
    func setValue(by rawData: RawData) {
        name = rawData.location
        notes = rawData.locationNotes
        latitude = Double(rawData.lat)!
        longitude = Double(rawData.long)!
    }
    
    func isSame(contentOf rawData: RawData) -> Bool {
        return latitude == Double(rawData.lat)!
            && longitude == Double(rawData.long)!
            && name == rawData.location
            && notes == rawData.locationNotes
    }
}

extension Int16 {
    init?(_ valueStr: String) {
        var tmpResult = 0
        for eachBits in valueStr.unicodeScalars {
            let tmpInt = Int(eachBits.value)
            guard tmpInt > 47 && tmpInt < 58 else { break }
            tmpResult *= 10
            tmpResult += tmpInt - 48
        }
        self.init(tmpResult)
    }
}

