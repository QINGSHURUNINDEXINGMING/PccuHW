#include <iostream>
#include <string>
using namespace std;

int main()
{
	int type, key;
	string s1;

	cout << "Encoding ( please press 1 ) or decoding ( please press 2 ) ?: ", cin >> type, cout << endl;

	cout << "Please enter the key: ", cin >> key, cout << endl;

	cin.ignore(1000, '\n');

	cout << "Please enter the string(Plaintext with lowercase and ciphertext with uppercase): ", getline(cin, s1);

	if (type==1)
	{
		cout << "\nEncoding is : ";
		for (int i = 0; i < s1.length(); i++) cout << char((((s1[i] - 'a') + key + 26) % 26) + 'A');
		
	}
	else if (type==2)
	{
		cout << "\nDecoding is : ";
		for (int i = 0; i < s1.length(); i++) cout << char((((s1[i] - 'A') - key + 26) % 26) + 'a');
	}

	cout << endl;
}
