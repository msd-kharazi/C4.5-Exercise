from collections import Counter
import math

class TreeNode:
    def __init__(self):
        self.__title = '' 
        self.__informationGain = 0
        self.__gainRatio = 0

      

    @property
    def title(self):
        return self.__title


    @title.setter   
    def title(self, value):
        self.__title = value




    @property
    def informationGain(self):
        return self.__informationGain


    @informationGain.setter   
    def informationGain(self, value):
        self.__informationGain = value



    @property
    def gainRatio(self):
        return self.__gainRatio


    @gainRatio.setter   
    def gainRatio(self, value):
        self.__gainRatio = value




    @property
    def children(self):
        return self.__children


    @children.setter
    def children(self, value):
        self.__children = value
          