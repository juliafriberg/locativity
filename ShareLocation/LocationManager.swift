import Foundation
import CoreLocation

class LocationManager: NSObject {
    static let shared = LocationManager()

    var locationManager: CLLocationManager?

    override init() {
        super.init()
        updateLocationTracking()
    }

    func updateLocationTracking() {
        let isSharing = UserDefaults.retrieveIsSharing()
        if (isSharing) {
            locationManager = CLLocationManager()
            locationManager?.delegate = self
            locationManager?.requestAlwaysAuthorization()
            locationManager?.allowsBackgroundLocationUpdates = true
        } else {
            locationManager?.stopMonitoringSignificantLocationChanges()
            locationManager?.stopUpdatingLocation()
        }
    }
}

extension LocationManager: CLLocationManagerDelegate {
    func locationManager(_ manager: CLLocationManager,
                         didChangeAuthorization status: CLAuthorizationStatus) {
        let isSharing = UserDefaults.retrieveIsSharing()
        guard isSharing else { return }

        if (status == CLAuthorizationStatus.denied) {
            // Show alert with preferences link
            UserDefaults.save(isSharing: isSharing)
            updateLocationTracking()
        } else if (status == CLAuthorizationStatus.authorizedAlways) {
            locationManager?.startMonitoringSignificantLocationChanges()
        } else if (status == CLAuthorizationStatus.authorizedWhenInUse) {
            locationManager?.startUpdatingLocation()
            locationManager?.distanceFilter = 50
        }
    }

    func locationManager(_ manager: CLLocationManager, didUpdateLocations locations: [CLLocation]) {
        guard let location = locations.last, let anonymousId = UserDefaults.retrieveID() else { return }

        let timestamp = location.timestamp.timeIntervalSince1970
        let savedLocation = Location(latitude: location.coordinate.latitude, longitude: location.coordinate.longitude, timestamp: Int(timestamp), id: anonymousId)
        API.post(coordinates: savedLocation)
    }
}
