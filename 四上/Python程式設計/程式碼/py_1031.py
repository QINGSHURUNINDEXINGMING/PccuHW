# print(range(100))
# -->range(100)

# print(9**2)
# -->81
# print(9**(1/2))
# -->3



# 求質數搭配開根號

num=int(input("Please enter specify the range(0-N): "))
for i in range(2,num+1):
    for j in range(2,int(num**0.5)+1):
        if(i%j==0 and i!=j):
            break
    else:
        print(i) 

# print(int(num**0.5)+1)


# 原版求質數無搭配開根號

# num=int(input("Please enter specify the range(0-N): "))
# for i in range(2,num+1):
#     for j in range(2,i):
#         if(i%j==0):
#             break
#     else:
#         print(i) 

# 不運作
# for i in range(2,2):
#     print(i)






