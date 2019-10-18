# s1 =()
# s2 =(())

# a1 =type(s1)
# print(a1)

# a2 =type(s2)
# print(a2)


# s =(((1, 2), ))
# a =len(s)
# print(a)

# print("Hello", end=' ')
# print("ABC")
# print("***")




fullname = input("Please enter your fullname: ");

space = fullname.find(" ");

name = (fullname[0:space], fullname[space+1:len(fullname)])

print(
      name[1][len(fullname[space + 1:len(fullname)])-2:len(fullname[space+1:len(fullname)])].capitalize() +
      name[0][0:2], 
      name[1][0:2].capitalize() + 
      name[0][len(fullname[0:space])-2:len(fullname[0:space])]
     )


#-2:從倒數第二個開始, -3從倒數第三個開始

