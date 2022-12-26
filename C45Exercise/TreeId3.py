import math
from TreeNode import TreeNode

class TreeId3:
    def calcEtropy(self,values):
        positiveCount = 0
        negativeCount = 0
        for value in values:
            if (value == True):
                positiveCount+=1
            else:
                negativeCount+=1
        
        positiveRate = (positiveCount/len(values))
        negativeRate = (negativeCount/len(values))
        result = -1 *( (positiveRate*math.log2(positiveRate)) + (negativeRate*math.log2(negativeRate)))

        return result

   
    def getTree(self,dataframe,targetColumnIndex):
        targetValues = []
        
        for row in range(1, dataframe.max_row): 
            for col in dataframe.iter_cols(targetColumnIndex, targetColumnIndex):
                targetValues.append(col[row].value)


        hs = self.calcEtropy(targetValues)
        print("This is entropy: " + str(hs))

        remainedColumnIndexes = []
        for counter in range(1,dataframe.max_column):
            if (counter != targetColumnIndex):
                remainedColumnIndexes.append(counter)

        print(remainedColumnIndexes)
        
        columnNames = []
        for col in dataframe.iter_cols(1, dataframe.max_column):
            columnNames.append(col[0].value)
                 
        data = [] 
        
        for row in range(0, dataframe.max_row):
            rowValues = []
            for col in dataframe.iter_cols(1, dataframe.max_column):
                rowValues.append(col[row].value)

            data.append(rowValues)
        



        result = self.CreateTree(data,targetColumnIndex-1,hs)

        #for row in range(0, dataframe1.max_row):
        #    for col in dataframe1.iter_cols(1, dataframe1.max_column):
        #        print(col[row].value)


    def CreateTree(self,data,targetColumnIndex,hs):
        
        #check if all target values are same
        targetValues = []
        
        for rowCounter in range(1, len(data)): 
            if data[rowCounter][targetColumnIndex] not in targetValues):
                targetValues.append(data[rowCounter][targetColumnIndex])


        if len(targetValues) == 1:
            #Create a leaf with the single value of targetValues[0]
            leaf = TreeNode()
            leaf.title = targetValues
            return leaf
        
        #Find best attribute
        attributeInformationGains = []
        for attributeCounter in range(0,targetColumnIndex): 
            columnData = []

            for rowCounter in range(0, len(data)): 
                newRow = [data[rowCounter][attributeCounter],data[rowCounter][targetColumnIndex]]
                columnData.append(newRow) 
             
            gain = self.calcGain(columnData,hs)
            attributeInformationGains.append(gain)

        #Create node with related children (possible values)
        #Create a tree for each child (possible value)
        
    def calcGain(self, columnData,hs):


        