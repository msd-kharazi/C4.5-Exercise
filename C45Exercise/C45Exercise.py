import os
import sys 
import openpyxl
from TreeId3 import TreeId3

 
# Define variable to load the dataframe
dataframe = openpyxl.load_workbook(os.getcwd() + r'\input 1.xlsx')
 
# Define variable to read sheet
dataframe1 = dataframe.active
 
 
#targetColumnIndex = input("Please enter target column index (starting from 1):")
targetColumnIndex = 5
id3 = TreeId3()
id3.getTree(dataframe1,int(targetColumnIndex))
 


# Iterate the loop to read the cell values
#for row in range(0, dataframe1.max_row):
#    for col in dataframe1.iter_cols(1, dataframe1.max_column):
#        print(col[row].value)