//
//  UrlModel.swift
//  assignment01
//
//  Created by aoo' Mac Mini on 10/11/2018.
//  Copyright © 2018 COMP327. All rights reserved.
//

import Foundation

// MARK: - Structure
/* The structure is used to store raw data of a report */
struct RawReportData: Decodable {
    let year: String
    let id: String
    let owner: String?
    let authors: String
    let title: String
    let abstract: String?
    let pdf: URL?
    let comment: String?
    let lastModified: String
}

/* The structure is used to store reports in a website */
struct WebpageData: Decodable{
    let techreports: [RawReportData]
}

// MARK: - Protocol
/* The protocol is used to declare the format of the data source for the report respoitory */
protocol ReportDataSource {
    var rawReportDataList:[RawReportData] {get}
    var identification: String {get}
}

// MARK: - Class
/* The report website is a kind of data source */
class ReportWebsite:ReportDataSource{
    
    var rawReportDataList: [RawReportData]
    var identification: String
    
    init(url: URL) {
        identification = url.absoluteString
        var tmpList = [RawReportData]()
        let request = URLRequest(url: url)
        let session = URLSession.shared
        let semaphore = DispatchSemaphore(value: 0)
        let dataTask = session.dataTask(with: request, completionHandler: {(data, response, error) -> Void in
            guard let jsonData = data else {return}
            do{
                let decoder = JSONDecoder()
                let webPage = try decoder.decode(WebpageData.self, from: jsonData)
                tmpList = webPage.techreports
            } catch let jsonErr {
                print("ReportWebsit.init(URL):Error decoding JSON", jsonErr)
                print(String(url.absoluteString))
            }
            semaphore.signal()
        }) as URLSessionTask
        dataTask.resume()
        _=semaphore.wait(timeout: DispatchTime.distantFuture)
        rawReportDataList = tmpList
    }
}


