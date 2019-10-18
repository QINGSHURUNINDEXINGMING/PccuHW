'''
Class：外系來旁聽, 學生_邱郁涵

Homework_2：姓名重組
            輸入一個全名(firstName lastName)， 
            將姓的前兩個字母與名字的後兩個字母組成新姓， 
            將姓的後兩個字母與名字的前兩個字母組成新名， 
            印出新姓名。
            新的姓和名第一個字母大寫，其餘小寫

ID：A6409001

'''
fullname = input("Please enter your fullname: ");
space = fullname.find(" ");
name = (fullname[0:space], fullname[space+1:len(fullname)])

#-2:從倒數第二個開始, -3從倒數第三個開始
print(
      name[1][len(fullname[space + 1:len(fullname)])-2:len(fullname[space+1:len(fullname)])].capitalize() +
      name[0][0:2].lower(), 
      name[1][0:2].capitalize() + 
      name[0][len(fullname[0:space])-2:len(fullname[0:space])].lower()
     )



