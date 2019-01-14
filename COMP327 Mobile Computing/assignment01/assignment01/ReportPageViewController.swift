//
//  ReportPageViewController.swift
//  assignment01
//
//  Created by aoo' Mac Mini on 11/11/2018.
//  Copyright © 2018 COMP327. All rights reserved.
//

import UIKit

/* View controller for the report page */
class ReportPageViewController: UIViewController {

    // MARK: - Variables
    /* the input report of the page */
    var inputReport: Report? = nil
    /* UI variables */
    @IBOutlet weak var reportTitle: UITextView!
    @IBOutlet weak var reportAuthorName: UITextView!
    @IBOutlet weak var reportDescription: UITextView!
    @IBOutlet weak var switchButton: UISwitch!
    
    // MARK: - Function
    /* function after click the done button in top bar*/
    @IBAction func clickDoneButton(_ sender: Any) {
        self.presentingViewController!.dismiss(animated: true, completion: nil)
    }

    /* function click the readModel button in the top bar*/
    @IBAction func enterReadModel(_ sender: Any) {
        guard let urlStr = inputReport?.pdf, let url = NSURL(string:urlStr) else{ return }
        UIApplication.shared.open(url as URL, options: [:], completionHandler: nil)
    }
    
    /* function after click the switch button */
    @IBAction func marketAsFavourit(_ sender: Any) {
        let appDelegate = UIApplication.shared.delegate as! AppDelegate
        let context = appDelegate.persistentContainer.viewContext
        ReportRespoitory(in: context).changeFavourite(report: inputReport!)
    }
    
    /* function used to initiate the page */
    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view.
        reportTitle.text = inputReport?.title ?? ""
        reportAuthorName.text = inputReport?.authors ?? ""
        reportDescription.text = (inputReport?.abstract ?? "").trimmingCharacters(in: .newlines)
        switchButton.setOn(inputReport?.isFavour ?? false, animated: false)
    }
    

    /*
    // MARK: - Navigation

    // In a storyboard-based application, you will often want to do a little preparation before navigation
    override func prepare(for segue: UIStoryboardSegue, sender: Any?) {
        // Get the new view controller using segue.destination.
        // Pass the selected object to the new view controller.
    }
    */

}
