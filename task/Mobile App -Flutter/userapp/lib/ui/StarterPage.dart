// ignore_for_file: library_private_types_in_public_api

import 'package:flutter/material.dart';
import 'package:userapp/service/AuthService.dart';

class StarterPage extends StatefulWidget {
  const StarterPage({super.key});

  @override
  _StarterPageState createState() => _StarterPageState();
}

class _StarterPageState extends State<StarterPage> {
  final AuthService _authService = AuthService();

  TextEditingController emailController =
      TextEditingController(); // Controller for email
  TextEditingController passwordController =
      TextEditingController(); // Controller for password

  @override
  void dispose() {
    // Dispose the controllers when the widget is disposed
    emailController.dispose();
    passwordController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Login Page'),
      ),
      body: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        crossAxisAlignment: CrossAxisAlignment.center,
        children: <Widget>[
          Container(
            height: 150.0,
            width: 190.0,
            padding: const EdgeInsets.only(top: 40),
            decoration: BoxDecoration(
              borderRadius: BorderRadius.circular(200),
            ),
            child: Center(
              child: Image.asset('images/flutter-logo.png'),
            ),
          ),
          Padding(
            padding: const EdgeInsets.all(10),
            child: TextField(
              controller: emailController,
              decoration: const InputDecoration(
                  border: OutlineInputBorder(),
                  labelText: 'User Name',
                  hintText: 'Enter valid mail id as abc@gmail.com'),
            ),
          ),
          Padding(
            padding: const EdgeInsets.all(10),
            child: TextField(
              controller: passwordController,
              obscureText: true,
              decoration: const InputDecoration(
                  border: OutlineInputBorder(),
                  labelText: 'Password',
                  hintText: 'Enter your secure password'),
            ),
          ),
          TextButton(
            onPressed: () {
              //Navigator.push(context, MaterialPageRoute(builder: (_) => ForgotPasswordPage()));
            },
            child: const Text(
              'Forgot Password',
              style: TextStyle(color: Colors.blue, fontSize: 15),
            ),
          ),
          Container(
            height: 50,
            width: 250,
            decoration: BoxDecoration(
              color: Colors.blue,
              borderRadius: BorderRadius.circular(20),
            ),
            child: MaterialButton(
              onPressed: () {
                String email =
                    emailController.text.trim(); // Retrieve email value
                String password =
                    passwordController.text.trim(); // Retrieve password value
                _authService.fetchDataFromAPI(email, password);
              },
              child: const Text(
                'Login',
                style: TextStyle(color: Colors.white, fontSize: 25),
              ),
            ),
          ),
          // Other widgets for login form, buttons, etc.
        ],
      ),

      // Other widgets and configurations for the login page...
    );
  }
}
