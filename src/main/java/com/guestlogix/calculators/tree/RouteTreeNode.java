package com.guestlogix.calculators.tree;

import com.guestlogix.database.entities.Airport;
import com.guestlogix.database.entities.Route;

import java.util.List;

/**
 * This class represents a tree node for the routes that will be handled in a tree algorithm.
 *
 * @author Vanderson Assis
 * @since 4/30/2019
 */

public class RouteTreeNode {
    private Route route;
    private List<Route> connectingRoutes;
    private RouteTreeNode parentNode;
    private RouteTreeNode siblingNode;
    private boolean isRoot;

    RouteTreeNode(Route route, List<Route> connectingRoutes, RouteTreeNode parentNode, RouteTreeNode siblingNode, boolean isRoot) {
        this.route = route;
        this.connectingRoutes = connectingRoutes;
        this.parentNode = parentNode;
        this.siblingNode = siblingNode;
        this.isRoot = isRoot;
    }

    public boolean hasSibling() {
        return this.siblingNode != null;
    }

    //getters and setters (whenever you see this comment in my code, means that bellow methods are nothing more than default getters and setters, so no need to analyze them)s
    public Route getRoute() {
        return route;
    }

    public void setRoute(Route route) {
        this.route = route;
    }

    public List<Route> getConnectingRoutes() {
        return connectingRoutes;
    }

    public void setConnectingRoutes(List<Route> connectingRoutes) {
        this.connectingRoutes = connectingRoutes;
    }

    public RouteTreeNode getParentNode() {
        return parentNode;
    }

    public void setParentNode(RouteTreeNode parentNode) {
        this.parentNode = parentNode;
    }

    public boolean isRoot() {
        return isRoot;
    }

    public void setRoot(boolean root) {
        isRoot = root;
    }

    public RouteTreeNode getSiblingNode() {
        return siblingNode;
    }

    public void setSiblingNode(RouteTreeNode siblingNode) {
        this.siblingNode = siblingNode;
    }
}
