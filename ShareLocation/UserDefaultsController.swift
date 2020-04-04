import Foundation

enum UserDefaultKeys: String {
    case anonymousID, isSharing
}

extension UserDefaults {
    static func save(id: String) {
        standard.set(id, forKey: UserDefaultKeys.anonymousID.rawValue)
    }

    static func retrieveID() -> String? {
        return standard.string(forKey: UserDefaultKeys.anonymousID.rawValue)
    }

    static func save(isSharing: Bool) {
        standard.set(isSharing, forKey: UserDefaultKeys.isSharing.rawValue)
    }

    static func retrieveIsSharing() -> Bool {
        return standard.bool(forKey: UserDefaultKeys.isSharing.rawValue)
    }
}
