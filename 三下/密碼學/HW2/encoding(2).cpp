#define _CRT_SECURE_NO_DEPRECATE

#include <iostream>
#include <istream>
#include <iomanip>
#include <fstream>
#include <cstring>
#include <string>
#include <cstdio>

using namespace std;

int main()
{
	fstream plaintext("C:\\Users\\user\\Desktop\\±K½X¾Ç\\HW2\\plaintext(2).txt", ios::in);
	fstream keySequence("C:\\Users\\user\\Desktop\\±K½X¾Ç\\HW2\\keySequence(2).txt", ios::in);
	fstream ciphertext("C:\\Users\\user\\Desktop\\±K½X¾Ç\\HW2\\ciphertext(4).txt", ios::out);

	string s1;
	int key;

	if (plaintext.is_open() && keySequence.is_open())
	{
		while (getline(plaintext, s1, (char)plaintext.eof()))
		{
			cout << "plaintext: " << s1 << endl;
		}

		keySequence >> key;
		cout << "keySequence: " << key << endl;

		plaintext.close();
		keySequence.close();
	}
	else
	{
		cout << "file isn't open" << endl;
	}

	cout << "ciphertext: ";

	char *c1 = new char[s1.length() + 1];
	strcpy(c1, s1.c_str());

	for (int i = 0; i < s1.length(); i++)
	{
		if (c1[i] >= 'A'&&c1[i] <= 'Z' || c1[i] >= 'a'&&c1[i] <= 'z')
		{
			cout << char((((c1[i] - 'a') + (key) + 26) % 26) + 'A');
			ciphertext << char((((c1[i] - 'a') + (key) + 26) % 26) + 'A');
		}
		else
		{
			cout << c1[i];
			ciphertext << c1[i];
		}
	}

	cout << "\n\n";

	ciphertext.close();

	cin.get();
}