//
//  WebsiteDownloader.swift
//  assignment02
//
//  Created by aoo' Mac Mini on 14/12/2018.
//  Copyright © 2018 COMP327. All rights reserved.
//

import Foundation

/*
 The class is the model of online data downloader.
 It implements the singleton and factory design pattern.
 */
class DownLoader{
    /* singleton */
    static let shared = DownLoader()
    
    // MARK:- Varables
    private let imagePath: String
    private let sourcePath: String
    /* local log file */
    private var logFile: [String: String] {
        get{
            if SystemSetting.shared.getSetting(by: "clearing local data each time")! {
                print("DownLoader.getter: Website record has been cleared")
                return [String: String]()
            }
            let logPath = NSHomeDirectory() + "/Documents/updateLog.plist"
            let tmpFile = NSDictionary(contentsOfFile: logPath) as? [String: String]
            print("DownLoader.logFile.getter: log file has been obtained")
            return tmpFile ?? [String: String]()
        }
        set{
            let logPath = NSHomeDirectory() + "/Documents/updateLog.plist"
            (newValue as NSDictionary).write(toFile: logPath, atomically: true)
            print("DownLoader.logFile.setter: log file has been store in local")
        }
    }
    // MARK:- Public Interface
    /* get all data in source website */
    func downloadWebsite() -> WebDataSource?{
        var requestPath = sourcePath
        var tmpLogDictionary = logFile
        if tmpLogDictionary.keys.contains(sourcePath) {
            let lastUpdateDateStr = logFile[sourcePath]!
            requestPath += "&lastUpdate=\(lastUpdateDateStr)"
        }
        let currentDateStr = Date.todayStr
        tmpLogDictionary[sourcePath] = currentDateStr
        logFile = tmpLogDictionary
        guard let url = URL(string:requestPath) else { return nil }
        guard let jsonData = downloadSync(from: url) else { return nil }
        return WebDataSource(jsonData)
    }
    
    /* Download image and cache the result of downloading request */
    func downloadImageAndCache(with fileName: String) -> Data? {
        guard var tmpURL = URL(string: imagePath) else { return nil }
        tmpURL.appendPathComponent(fileName)
        return downloadSync(from: tmpURL)
    }
    
    // MARK:- Private Methods
    private init(){
        let absURLLocalPath = Bundle.main.path(forResource: "sourcesWebsite", ofType: "plist")!
        let tmpPathDictionary = NSDictionary(contentsOfFile: absURLLocalPath) as! [String: String]
        imagePath = tmpPathDictionary["imgPath"]!
        sourcePath = tmpPathDictionary["srcPath"]!
        if SystemSetting.shared.getSetting(by: "using image cache")!{
            let memorySize = 50*1024*1024
            let diskSize = 100*1024*1024
            URLCache.shared = URLCache(memoryCapacity: memorySize, diskCapacity: diskSize, diskPath: "imageCache")
            print("Downloader.init(): Image Cache has been initiated")
        }
    }
    
    private func downloadSync(from url: URL) -> Data?{
        var rstData: Data?
        let request = URLRequest(url: url)
        let semaphore = DispatchSemaphore(value: 0)
        let dataTask = URLSession.shared.dataTask(with: request){ (data, response, error) in
            defer {semaphore.signal()}
            guard error == nil else { return }
            guard (response as? HTTPURLResponse)?.statusCode == 200 else { return }
            rstData = data
        }
        dataTask.resume()
        _=semaphore.wait(timeout: DispatchTime.distantFuture)
        return rstData
    }
}

extension Date{
    static var todayStr: String {
        let formatter = DateFormatter()
        formatter.dateFormat = "yyyy-MM-dd"
        return formatter.string(from: Date())
    }
}
