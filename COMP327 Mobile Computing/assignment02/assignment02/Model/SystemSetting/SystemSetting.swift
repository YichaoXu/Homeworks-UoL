//
//  SystemSetting.swift
//  assignment02
//
//  Created by aoo' Mac Mini on 15/12/2018.
//  Copyright © 2018 COMP327. All rights reserved.
//

import Foundation

/*
 The class is the model of system setting page.
 It implements the singleton design pattern.
 */
class SystemSetting{
    /* Singleton */
    static let shared = SystemSetting()
    /* The system settings */
    private var settingFile:[String: Bool]
    
    private init(){
        //obtian system setting from a local file
        let defaultSettingPath = Bundle.main.path(forResource: "defaultSetting", ofType: "plist")!
        settingFile = NSDictionary(contentsOfFile: defaultSettingPath) as! [String: Bool]
        print("SystemSetting.init(): Setting file has been loaded")
        for eachSetting in settingFile {
            print("\t\(eachSetting.key): \(eachSetting.value)")
        }
    }
    
    func getAllSetting() -> [String: Bool] {
        return settingFile
    }
    
    func getSetting(by key: String) -> Bool? {
        return settingFile[key]
    }
    
    /* Update the setting to a new value and store the value in local */
    func update(key: String, value: Bool) {
        // Update the setting
        settingFile[key] = value
        // Update local file
        let defaultSettingPath = Bundle.main.path(forResource: "defaultSetting", ofType: "plist")!
        (settingFile as NSDictionary).write(toFile: defaultSettingPath, atomically: true)
    }
}
