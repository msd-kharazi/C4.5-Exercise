import os
import sys 
import openpyxl
from TreeId3 import TreeId3
from TreeC45 import TreeC45


dataframe = openpyxl.load_workbook(os.getcwd() + r'\input 1.xlsx')
 
dataframe1 = dataframe.active
 
 
print('File 1 ID3:\n\n\n\n')
targetColumnIndex = 5
id3 = TreeId3()
id3.getTree(dataframe1,int(targetColumnIndex))

print('File 1 C45:\n\n\n\n')
c45 = TreeC45()
c45.getTree(dataframe1,int(targetColumnIndex))



dataframe = openpyxl.load_workbook(os.getcwd() + r'\input 2.xlsx')
 
dataframe1 = dataframe.active
 
 
print('File 2 ID3:\n\n\n\n')
targetColumnIndex = 4
id3 = TreeId3()
id3.getTree(dataframe1,int(targetColumnIndex))

print('File 2 C45:\n\n\n\n')
c45 = TreeC45()
c45.getTree(dataframe1,int(targetColumnIndex))