import Foundation

class API {
    static let domainURL: String = "https://hackcrisisapi.azurewebsites.net/api/"

    static func fetchID(completionHandler: @escaping (String) -> Void) {
        let url = URL(string: domainURL + "Identifier/")!

        let task = URLSession.shared.dataTask(with: url, completionHandler: { (data, response, error) in
            if let error = error {
                print("Error with fetching identifier: \(error)")
                return
            }

            guard let httpResponse = response as? HTTPURLResponse,
                (200...299).contains(httpResponse.statusCode) else {
                    print("Error with the response, unexpected status code: \(response)")
                    return
            }

            if let data = data,
                let identifier = try? JSONDecoder().decode(Identifier.self, from: data) {
                completionHandler(identifier.id)
            }
        })
        task.resume()
    }

    static func post(coordinates: Location) {
        print("posting")
        let url = URL(string: domainURL + "Coordinates/")!

        var request = URLRequest(url: url)
        request.httpMethod = "POST"
        request.setValue("Application/json", forHTTPHeaderField: "Content-Type")

        let jsonData = try! JSONEncoder().encode(coordinates)
        let task = URLSession.shared.uploadTask(with: request, from: jsonData) { (data, response, error) in
            if let error = error {
                print("Error with fetching identifier: \(error)")
                return
            }

            guard let httpResponse = response as? HTTPURLResponse,
                (200...299).contains(httpResponse.statusCode) else {
                    print("Error with the response, unexpected status code: \(response)")
                    return
            }
        }
        print(task.originalRequest)
        task.resume()
    }
}
