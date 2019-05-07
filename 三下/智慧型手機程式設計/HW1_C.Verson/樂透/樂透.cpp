#include<iostream>
#include <string>
#include <sstream>
#include <cstdlib>
#include <ctime>
#include <algorithm>

using namespace std;

string twodigit(int);
string genLotteryNum(int, int);
bool exist(int[], int, int);

int main()
{
	ios_base::sync_with_stdio(false);
	cin.tie(0);

	srand(time(0));

	int type = 0, times = 0;
	cin >> type >> times;
	cout << genLotteryNum(type, times);

	system("pause");
}

//輸入型別
string genLotteryNum(int type, int times)
{
	int number1, count, number2, biggestNum;
	bool flag;
	int lotteryNum[6] = {0};
	string result;

	//判段大樂透還是威力彩
	if (type == 0)
	{
		biggestNum = 49;
		stringstream ss;
		ss << times;
		result = "大樂透" + ss.str() + "組號碼:\r\n";
	}
	else
	{
		biggestNum = 38;
		stringstream ss;
		ss << times;
		result = "威力彩" + ss.str() + "組號碼:\r\n";
		result += "              第一區           第二區\r\n";
	}


	//產生數字
	for (int i = 0; i < times; i++)
	{
		int count = 0;

		for (int j = 0; j < 6; j++) lotteryNum[j] = 0;

		do
		{
			number1 = rand() % biggestNum + 1;
			flag = exist(lotteryNum, count, number1);
			if (flag==false)
			{
				lotteryNum[count] = number1;
				count++;
			}

		} while (count<6);

		//排列數字
		sort(lotteryNum, lotteryNum + 6);
		//數字儲存自字串
		for (int k = 0; k < 6; k++)	result += twodigit(lotteryNum[k]) + "    ";
		
		if (type == 0) result += "\r\n";
		else
		{
			number2 = rand() % 8 + 1;
			stringstream ss;
			ss << number2;
			result += "      " + ss.str() + "\r\n";
		}
	}
	return result;
}

//旗標
bool exist(int numArrary[], int count, int number)
{
	bool status = false;
	for (int i = 0; i < count; i++)
	{
		if (numArrary[i] == number)
		{
			status = true;
			break;
		}
	}
	return status;
}

//轉字串
string twodigit(int number)
{
	int digit2 = number / 10;
	int digit1 = number % 10;

	stringstream ss;
	ss << digit2 << digit1;

	return ss.str();
}