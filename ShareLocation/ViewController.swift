import UIKit
import CoreLocation

class ViewController: UIViewController {
    @IBOutlet var shareButton: UIButton!
    @IBOutlet var idLabel: UILabel!

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
        shareButton.setTitle(isSharing ? "Stop" : "Start", for: .normal)
    }

    private func updateAnonymousCodeText() {
        idLabel.text = "Your anonymous code is \(anonymousCode)"
    }
}


