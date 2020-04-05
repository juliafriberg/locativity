import Foundation

extension Date {
    var isoFormatted: String {
        let formatter = ISO8601DateFormatter()
        return formatter.string(from: self)
    }
}
