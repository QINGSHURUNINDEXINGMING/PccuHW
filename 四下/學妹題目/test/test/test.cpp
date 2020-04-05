// test.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include <algorithm>
#include <vector>
#include <stdlib.h> 
#include <time.h>   

using namespace std;

void swap(int* x, int* y)
{
	int t;
	t = *x, *x = *y, *y = t;
}

int main()
{
	char c;
	int n1 = 0, n2 = 0;

	vector<vector<vector<int>>>v(2, vector<vector<int>>(3, vector<int>(3)));
	vector<vector<int>>compareTwoPosition(2, vector<int>());

	for (int k = 0; k < v.size(); k++)
	{
		for (int i = 0; i < v[k].size(); i++)
		{
			for (int j = 0; j < v[k][i].size(); j++) {
				cin >> c, c == '*' ? v[k][i][j] = 1 : v[k][i][j] = 0;
				if (v[k][i][j] == 0 && k == 0)
				{
					n1++;
					compareTwoPosition[k].push_back(i);
					compareTwoPosition[k].push_back(j);
				}
				else if (v[k][i][j] == 0 && k == 1)
				{
					n2++;
					compareTwoPosition[k].push_back(i);
					compareTwoPosition[k].push_back(j);
				}
			}
		}
	}

	//if (equal(v[0].begin(), v[0].end(), v[1].begin()))
	//	cout << "YES" << endl;
	if ((n1 + n2) != 9) {
		cout << "NO";
	}

	for (int k = 0; k < 2; k++)
	{
		for (int i = 0; i < compareTwoPosition[0].size(); i++)
		{
			cout << compareTwoPosition[k][i] << " ";
		}
		cout << endl;
	}

	cout << endl;
	
	swap(&v[0][0][0], &v[0][2][2]);
	for (int i = 0; i < v[0].size(); i++)
	{
		for (int j = 0; j < v[0][i].size(); j++)
		{
			cout << v[0][i][j] << " ";
		}
		cout << endl;

	}
	cout << endl;





	//int number;
	//cin >> number;

	//while (number--)
	//{
	//	int kind, dollar;
	//	int result = 0;

	//	cin >> kind >> dollar;

	//	int* arr1 = new int[kind];
	//	int* arr2 = new int[kind];
	//	int* arr3 = new int[kind];

	//	for (int i = 0; i < kind; i++)
	//	{
	//		cin >> arr1[i];
	//	}

	//	sort(arr1, arr1 + kind);

	//	for (int i = kind - 1, j = 0; j < kind; i--, j++)
	//	{
	//		if (j == 0) {
	//			arr2[j] = dollar / arr1[i];
	//			arr3[j] = dollar % arr1[i];
	//		}
	//		else
	//		{
	//			arr2[j] = arr3[j - 1] / arr1[i];
	//			arr3[j] = arr3[j - 1] % arr1[i];
	//		}
	//	}

	//	for (int i = 0; i < kind; i++)
	//	{
	//		arr2[i] == 0 ? result += 0 : result += arr2[i];
	//	}

	//	result > 0 ? cout << result : cout << -1;
	//}



	/*int number;
	int sum = 0;
	cin >> number;

	int *arr = new int[number];

	for (int i = 0; i < number; i++)
	{
		cin >> arr[i];
		sum += arr[i];
	}

	cout << sum;*/

}

// Run program: Ctrl + F5 or Debug > Start Without Debugging menu
// Debug program: F5 or Debug > Start Debugging menu

// Tips for Getting Started: 
//   1. Use the Solution Explorer window to add/manage files
//   2. Use the Team Explorer window to connect to source control
//   3. Use the Output window to see build output and other messages
//   4. Use the Error List window to view errors
//   5. Go to Project > Add New Item to create new code files, or Project > Add Existing Item to add existing code files to the project
//   6. In the future, to open this project again, go to File > Open > Project and select the .sln file
