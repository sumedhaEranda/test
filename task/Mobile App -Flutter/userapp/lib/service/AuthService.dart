import 'dart:convert'; // Import the dart:convert library
import 'package:http/http.dart' as http;

class AuthService {
  final String apiUrl = 'http://localhost:50006/Home/Index';

  Future<bool> fetchDataFromAPI(String username, String password) async {
    try {
      final response = await http.post(
        Uri.parse(apiUrl),
        headers: <String, String>{
          'Content-Type': 'application/json; charset=UTF-8',
          // Add any other necessary headers here
        },
        body: jsonEncode(<String, String>{
          'Username': username,
          'password': password,
        }),
      );

      if (response.statusCode == 200) {
        print('Authentication successful for $response');

        return true;
      } else {
        print('Authentication failed for $username');
        return false;
      }
    } catch (e) {
      print('Error occurred: $e');
      return false;
    }
  }
}
