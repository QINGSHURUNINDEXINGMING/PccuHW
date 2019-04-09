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
	fstream ciphertext("C:\\Users\\user\\Desktop\\±K½X¾Ç\\HW2\\ciphertext(2).txt", ios::in);
	fstream keySequence("C:\\Users\\user\\Desktop\\±K½X¾Ç\\HW2\\keySequence(2).txt", ios::in);
	fstream plaintext("C:\\Users\\user\\Desktop\\±K½X¾Ç\\HW2\\plaintext(2).txt", ios::out);

	string s1;
	int key;

	if (ciphertext.is_open() && keySequence.is_open())
	{
		while (getline(ciphertext, s1, (char)ciphertext.eof()))
		{
			cout << "ciphertext: " << s1 << endl;
		}

		keySequence >> key;
		cout << "keySequence: " << key << endl;

		ciphertext.close();
		keySequence.close();
	}
	else
	{
		cout << "file isn't open" << endl;
	}

	cout << "plaintext: ";

	char *c1 = new char[s1.length() + 1];
	strcpy(c1, s1.c_str());

	for (int i = 0; i < s1.length(); i++)
	{
		if (c1[i] >= 'A'&&c1[i] <= 'Z' || c1[i] >= 'a'&&c1[i] <= 'z')
		{
			cout << char((((c1[i] - 'A') - (key) + 26) % 26) + 'a');
			plaintext << char((((c1[i] - 'A') - (key) + 26) % 26) + 'a');
		}
		else
		{
			cout << c1[i];
			plaintext << c1[i];
		}
	}

	cout << "\n\n";

	plaintext.close();

	cin.get();
}