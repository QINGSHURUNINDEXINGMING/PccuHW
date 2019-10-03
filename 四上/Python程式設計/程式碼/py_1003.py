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

print((name[1][0:2]+name[0][-1:-3]).capitalize())
print((name[0][-1:3]))
a=(name[0])
print(type(a))
print(a[-1:-3])

scrept="rennlrn6548"
print(scrept[-1:-3])





