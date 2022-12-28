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

        result = 0

        if positiveRate!=0:
            result = positiveRate*math.log2(positiveRate)

        if negativeRate!=0:
            result += negativeRate*math.log2(negativeRate)


        result *= -1 

        return result

   
    def getTree(self,dataframe,targetColumnIndex):
        targetValues = []
        
        for row in range(1, dataframe.max_row): 
            for col in dataframe.iter_cols(targetColumnIndex, targetColumnIndex):
                targetValues.append(col[row].value)


        hs = self.calcEtropy(targetValues)

        remainedColumnIndexes = []
        for counter in range(1,dataframe.max_column):
            if (counter != targetColumnIndex):
                remainedColumnIndexes.append(counter)

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
        self.customPrint(result,0)
 

    def CreateTree(self,data,targetColumnIndex,hs):
        
        #check if all target values are same
        targetValues = []
        
        for rowCounter in range(1, len(data)): 
            if data[rowCounter][targetColumnIndex] not in targetValues:
                targetValues.append(data[rowCounter][targetColumnIndex])


        if len(targetValues) == 1:
            #Create a leaf with the single value of targetValues[0]
            leaf = TreeNode()
            leaf.title =str(targetValues[0])
            leaf.children = []
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
            
        maxGain = max(attributeInformationGains)
        attributeIndex = attributeInformationGains.index(maxGain)

        root = TreeNode()
        root.title = data[0][attributeIndex] + ' - Information gain: ' + str(maxGain)
        root.children = []



        possibleValues = set()
        
        for rowCounter in range(1, len(data)): 
            possibleValues.add(data[rowCounter][attributeIndex]) 
             

        for possibleValue in possibleValues:               
            thisPossibleValueData = [] 
        
            for row in range(0, len(data)):
                if row != 0 and data[row][attributeIndex] != possibleValue:
                    continue

                rowValues = []
                for col in range(0,attributeIndex):
                    rowValues.append(data[row][col])

                for col in range(attributeIndex+1,len(data[0])):
                    rowValues.append(data[row][col])



                thisPossibleValueData.append(rowValues)
         
            targetValuesForEntropy = []
        
            for row in range(1, len(thisPossibleValueData)): 
                targetValuesForEntropy.append(thisPossibleValueData[row][targetColumnIndex-1]) 
            
            newEntropy = self.calcEtropy(targetValuesForEntropy) 
            possibleValueChildNode = self.CreateTree(thisPossibleValueData,targetColumnIndex-1,newEntropy)             
            possibleValueChildNode.title = '(' + possibleValue + ')' + ' - ' + possibleValueChildNode.title
            root.children.append(possibleValueChildNode)

        return root 
     
        
    def calcGain(self, data,hs):
        possibleValues = set()
        
        for rowCounter in range(1, len(data)): 
            possibleValues.add(data[rowCounter][0]) 
             

        totalEntropyInProbability = 0
        for possibleValue in possibleValues:
            count = 0
            positiveCount = 0
            negativeCount = 0
            for rowCounter in range(1, len(data)): 
                if data[rowCounter][0]==possibleValue:
                    count+=1
                    if data[rowCounter][1] == True:
                        positiveCount+=1
                    else:
                        negativeCount+=1

            positiveRate = positiveCount/count
            negativeRate = negativeCount/count
            possibleValueEntropy = 0
            
            if positiveRate != 0:
                possibleValueEntropy+= positiveRate*math.log2(positiveRate) 

            if negativeRate != 0:
                possibleValueEntropy+= negativeRate*math.log2(negativeRate)

            possibleValueEntropy*=-1

            totalEntropyInProbability+=(count/(len(data)-1)) * possibleValueEntropy
             
        attributeGain = hs-totalEntropyInProbability

        return attributeGain


    def customPrint(self,tree,level): 
        print(16 * level * ' '  + tree.title + '\n')
        if tree.children is not None:
            level+=1
            for child in tree.children:
                self.customPrint(child,level)
