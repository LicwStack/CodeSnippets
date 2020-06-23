import itertools as its
words0 = "0123456789abcdefghijklmnopqrstuvwxyz" #如需要，可加入大写字母及其他符号
words1 = "0123456789" #如需要，可加入大写字母及其他符号
dic = open('dictionary8.txt','w')
for num in range(8, 9): # 长度为8~10位数
    keys = its.product(words1, repeat=num)
    for key in keys:
        dic.write("".join(key)+"\n")

dic.close()