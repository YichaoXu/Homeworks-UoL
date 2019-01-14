//
//  WebsiteDataSource.swift
//  assignment02
//
//  Created by aoo' Mac Mini on 04/12/2018.
//  Copyright © 2018 COMP327. All rights reserved.
//

import Foundation
/*
 The class is the model of online data downloader.
 It implements the iterator design pattern.
 */
class WebDataSource: IteratorProtocol{
    
    private var rawDataList: [RawData]
    
    func next() -> RawData? {
        return rawDataList.popLast()
    }

    
    init? (_ jsonData: Data) {
        do{
            let rawWebsiteData = try JSONDecoder().decode(RawWebsiteData.self, from: jsonData)
            rawDataList = rawWebsiteData.artworks2
            print("WebDataSource.init(jsonData:): \(rawDataList.count) artworks data have been downloaded")
        } catch {
            print("WebDataSource.update(): \n", error)
            return nil
        }
    }
}

import Foundation

struct RawData: Decodable{
    let id: String
    let title: String
    let artist: String
    let yearOfWork: String
    let Information: String
    let fileName: String
    let location: String
    let lastModified: String
    let enabled: String
    let lat: String
    let long: String
    let locationNotes: String
}

struct RawWebsiteData: Decodable {
    let artworks2: [RawData]
}
