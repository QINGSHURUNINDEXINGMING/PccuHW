'''
Class：外系來旁聽, 學生_邱郁涵

Homework_3：從鍵盤讀進一個整數num1
            若此整數 > 50， 再讀入一個整數 num2，
            印出 num1 / num2 的值。
            否則，若此整數 < 10，則讀入一個字串，
            印出 num1 個此字串。
            否則，則讀入另一整數num2。
                  若此整數 > 5，印出 num1 + num2的值。
                  否則，印出 num1 * num2 的值。
ID：A6409001

'''


num1=int(input("Number1? "))
if num1 > 50:
      num2=int(input("NUmber2? "))
      print("num1 / num2 = ", num1//num2 )
elif num1 < 10:
      string1 = str(input("String? "))
      print("num1 * string1 = " + num1*string1)
else:
      num2=int(input("NUmber2? "))
      if num2 > 5:
            print("num1 + num2 = ", (num1+num2))
      else:
            print("num1 * num2 = ", (num1*num2))
70
