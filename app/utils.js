const csv = require('csv-parser');
const fs = require('fs');
const results = [];

function Stack(){
  this.stack = [];
}

Stack.prototype = {
  pop: function() {
    return this.stack.pop();
  },
  push: function(item){
    this.stack.push(item);
  },
  isEmpty: function(){
    return this.stack.length == 0;
  }
}

function Queue(){
  this.queue = [];
}

Queue.prototype = {
  enqueue: function(item) {
    return this.queue.push(item);
  },
  dequeue: function(){
    return this.queue.shift();
  },
  isEmpty: function(){
    return this.queue.length == 0;
  }
}

var Graph = function Graph() {
  this.vertices = [];
  this.adjacentList = new Map();
}

Graph.prototype = {

  addVertex: function(v) {
    if(this.vertices.indexOf(v) === -1)  {
      this.vertices.push(v);  
      this.adjacentList.set(v, []);
    }
  },

  getAdjacencyListVertex: function(vertex){
      return this.adjacentList.get(vertex) || [];
  },

  addEdge: function(u, v){
    var uVertex = this.getAdjacencyListVertex(u),
        vVertex = this.getAdjacencyListVertex(v);
    uVertex.push(v);
    vVertex.push(u);  
  },

  toString: function() {
    for (const [key, value] of this.adjacentList) {
      console.log(`${key} => ${value}`);
    }
  },
  
  breathFirstSearch: function(startingVertex, destination) {
    var color = [],
      distances = [],
      queue = new Queue(),
      self = this,
      preceding = [],

      setVertexColorsAndEdges = function() {
        for(var i = 0; i < self.vertices.length; i++) {
          color[self.vertices[i]] = 'white';
          distances[self.vertices[i]] = -1;

          if(self.vertices[i] === startingVertex) {
              distances[self.vertices[i]] = 0;
              preceding[self.vertices[i]] = startingVertex;
          } else {
              preceding[self.vertices[i]] = null;
          }  

        }
      };

    setVertexColorsAndEdges();
      queue.enqueue(startingVertex);
      while(!queue.isEmpty()) {

        var queueFrontVertex = queue.dequeue();

        color[queueFrontVertex] = 'grey';
        var frontVertexAdjLst = this.getAdjacencyListVertex(queueFrontVertex);
        frontVertexAdjLst.forEach(function(adjVertex){
            if(color[adjVertex] === 'white') {

                color[adjVertex] = 'grey';
                if(distances[queueFrontVertex] === -1) {
                    distances[queueFrontVertex] = 1;
                } else {
                    distances[adjVertex] = distances[queueFrontVertex] + 1;
                }
                preceding[adjVertex] = queueFrontVertex;
                queue.enqueue(adjVertex);
            }

        });
        color[queueFrontVertex] = 'black';
      }

      for (var i in distances) {
        if(destination == i) {
          console.log('Shortest Distance from ' + startingVertex + ' to ' + i + ' is in ' + distances[i] + ' step(s)');
        }
      }

      return {
        distances : distances,
        preceding : preceding
      };   

  },

  vertexExist: function(v) {
    return this.vertices.indexOf(v) >= 0;
  },

  shortestPathFromTo: function(bfs, source, destination) {
    if(!this.vertexExist(source) || !this.vertexExist(destination) ) {
      return console.log('Source or destination not found');
    }
    return this.shortest_path(bfs, source, destination);
  },

  shortest_path: function(bfs, source, destination) {
    var finalString,
        path = new Stack();

    if(!(source === destination)) {
        path.push(destination);
    } 
    
    var previousVertex = bfs.preceding[destination];
    //Perform backtracking
    while (previousVertex !== null && previousVertex !== source) {
      path.push(previousVertex);
      previousVertex = bfs.preceding[previousVertex];
    }
    
    if(previousVertex === null) {
        console.log('There is no path from ' + source + ' to ' + destination);
    } else { 
        path.push(source);
    }
    //print final results
    finalString = path.pop();
    while(!path.isEmpty()) {
        finalString += '-' + path.pop();
    }
    return finalString.split('-')
  }
}

module.exports = Graph;
