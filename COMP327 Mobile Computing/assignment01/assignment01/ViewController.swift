//
//  ViewController.swift
//  assignment01
//
//  Created by aoo' Mac Mini on 10/11/2018.
//  Copyright © 2018 COMP327. All rights reserved.
//

/*The Main page view Controller of the progame, it includes a table view.*/
import UIKit

class ViewController: UIViewController, UITableViewDataSource, UITableViewDelegate {
    // MARK: - Variables
    /*The table view of the main page*/
    @IBOutlet weak var table: UITableView!
    /*The reports respoitory of the application which stores information of all reports*/
    var reportRespoitory: ReportRespoitory?

    // MARK: - Function
    /* return year at section in table */
    private func year(at section: Int)->Int{
        let year = reportRespoitory?.getYearRange()[section] ?? 0
        return year
    }
    
    /* return report should be in indexPath from the report respoitory */
    private func report(at indexPath: IndexPath) -> Report {
        let year = self.year(at: indexPath.section)
        let tmpReport = reportRespoitory?.requireReports(In: year)[indexPath.row]
        return tmpReport!
    }
    
    /* return number of section */
    func numberOfSections(in tableView: UITableView) -> Int {
        return reportRespoitory?.getYearRange().count ?? 1
    }
    
    /* return number of rows in Section */
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        let year = self.year(at: section)
        return reportRespoitory?.requireNumberOfReports(In: year) ?? 0
    }

    /* return cell for row at indexPath*/
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let report = self.report(at: indexPath)
        let tmpCell = tableView.dequeueReusableCell(withIdentifier: "cell")!
        tmpCell.textLabel?.text = report.title
        tmpCell.detailTextLabel?.text = report.authors
        // set check mark
        if report.isFavour {
            tmpCell.accessoryType = .checkmark
        } else {
            tmpCell.accessoryType = .none
        }
        return tmpCell
    }

    /* return height for Header in section*/
    func tableView(_ tableView: UITableView, heightForHeaderInSection section: Int) -> CGFloat {
        return 40
    }
    
    /* return view for Header in section*/
    func tableView(_ tableView: UITableView, viewForHeaderInSection section: Int) -> UIView? {
        let headerView = UITableViewHeaderFooterView()
        headerView.textLabel?.text = String(reportRespoitory?.getYearRange()[section] ?? 0)
        return headerView
    }
    
    /* an action after the cell at indexPath is clicked*/
    func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
        tableView.deselectRow(at: indexPath, animated: true)
        self.performSegue(withIdentifier: "ReportPage", sender: self.report(at: indexPath))
    }
    
    /* prepares report:Report sent to the report page */
    override func prepare(for segue: UIStoryboardSegue, sender: Any?) {
        if segue.identifier == "ReportPage"{
            let reportPageController = segue.destination as! ReportPageViewController
            reportPageController.inputReport = sender as? Report
        }
    }

    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view, typically from a nib.
        let url = URL(string: "https://cgi.csc.liv.ac.uk/~phil/Teaching/COMP327/techreports/data.php?class=techreports")!
        let reportWebsite: ReportWebsite = ReportWebsite(url: url)
        let appDelegate = UIApplication.shared.delegate as! AppDelegate
        let context = appDelegate.persistentContainer.viewContext
        reportRespoitory = ReportRespoitory(in: context)
        reportRespoitory!.collectReports(from: reportWebsite)
    }
    
    override func viewWillAppear(_ animated: Bool) {
        table.reloadData()
    }
    


}

