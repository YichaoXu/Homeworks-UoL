//
//  SettingPageController.swift
//  assignment02
//
//  Created by aoo' Mac Mini on 15/12/2018.
//  Copyright © 2018 COMP327. All rights reserved.
//

import UIKit

/* the class is the controller for the system setting */
class SettingPageController: UITableViewController {
    
    /* The system settings */
    private var settingArray: [(key: String, value: Bool)]!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        settingArray = Array(SystemSetting.shared.getAllSetting())
    }

    override func numberOfSections(in tableView: UITableView) -> Int {
        return 1
    }

    override func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return settingArray.count
    }
    
    override func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let tmpCell = tableView.dequeueReusableCell(withIdentifier: "cell")!
        let tmpSetting = settingArray[indexPath.row]
        tmpCell.textLabel?.text = tmpSetting.key
        if tmpSetting.value {
            tmpCell.accessoryType = .checkmark
        } else {
            tmpCell.accessoryType = .none
        }
        return tmpCell
    }
    
    override func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
        tableView.deselectRow(at: indexPath, animated: true)
        var tmpSetting = settingArray[indexPath.row]
        tmpSetting.value = !tmpSetting.value
        settingArray[indexPath.row] = tmpSetting
        SystemSetting.shared.update(key: tmpSetting.key, value: tmpSetting.value)
        tableView.reloadRows(at: [indexPath], with: .right)
    }
    
}
