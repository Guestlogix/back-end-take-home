package com.Guestlogix.shortestRoute.domain.network.response;

import java.util.List;

/**
 * 
 * @author nchopra
 *
 */
public class DbListResponse<T> extends BaseResponse {

	private List<T> data;
	private Number id;

	public List<T> getData() {
		return data;
	}

	public void setData(List<T> data) {
		this.data = data;
	}

	public Number getId() {
		return id;
	}

	public void setId(Number id) {
		this.id = id;
	}

}
