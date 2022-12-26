from collections import Counter
import math

class TreeNode:
    def __init__(self):
        self.__title = '' 

    
    def __init__(self, title):
        self.__title=title


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
    def children(self):
        return self.__children


    @children.setter
    def title(self, value):
        self.__children = value
          