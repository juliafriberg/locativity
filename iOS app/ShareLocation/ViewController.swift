import UIKit
import CoreLocation

class ViewController: UIViewController {
    @IBOutlet var shareButton: UIButton!
    @IBOutlet var idLabel: UILabel!
    @IBOutlet var changeCodeButton: UIButton!

    let locationManager = LocationManager.shared

    var isSharing: Bool = false {
        didSet {
            UserDefaults.save(isSharing: isSharing)
            updateButtonText()
        }
    }
    var anonymousCode: String = "" {
        didSet {
            UserDefaults.save(id: anonymousCode)
            DispatchQueue.main.async {
                self.updateAnonymousCodeText()
            }
        }
    }

    override func viewDidLoad() {
        super.viewDidLoad()

        changeCodeButton.layer.cornerRadius = 12
        shareButton.layer.cornerRadius = 35

        if let id = UserDefaults.retrieveID() {
            anonymousCode = id;
        } else {
            API.fetchID { id in
                self.anonymousCode = id
            }
        }
        isSharing = UserDefaults.retrieveIsSharing()
        updateButtonText()
        updateAnonymousCodeText()
    }

    @IBAction func didTapShareButton(_ sender: Any) {
        isSharing.toggle()
        locationManager.updateLocationTracking()
    }

    @IBAction func didTapChangeCodeButton(_ sender: Any) {
        API.fetchID { id in
            self.anonymousCode = id
        }
    }

    private func updateButtonText() {
        shareButton.setImage(isSharing ? #imageLiteral(resourceName: "pause") : #imageLiteral(resourceName: "play"), for: .normal)
        shareButton.contentEdgeInsets.left = isSharing ? 20 : 24
    }

    private func updateAnonymousCodeText() {
        idLabel.text = anonymousCode
    }
}


