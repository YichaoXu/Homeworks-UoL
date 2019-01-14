//
//  ViewController.swift
//  assignment02
//
//  Created by aoo' Mac Mini on 30/11/2018.
//  Copyright © 2018 COMP327. All rights reserved.
//

import UIKit

/* the class is the controller of the detail page */
class DetailPageViewController: UIViewController {

    var artwork: Artwork!
    
    @IBOutlet weak var imageView: UIImageView!
    @IBOutlet weak var titleView: UITextView!
    @IBOutlet weak var authorView: UITextView!
    @IBOutlet weak var detailsView: UITextView!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        self.title = artwork.title
        DispatchQueue.main.async {
            guard let fileName = self.artwork.imgFileName else { return }
            guard let tmpData = DownLoader.shared.downloadImageAndCache(with: fileName) else { return }
            self.imageView.image = UIImage(data: tmpData)
        }
        titleView.text = artwork.title
        authorView.text = artwork.artist
        detailsView.text = artwork.information
    }
}

