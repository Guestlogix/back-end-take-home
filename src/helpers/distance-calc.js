export const distanceCalculation = (lat1, lon1, lat2, lon2) => {
	let distance = 0;

	if ((lat1 == lat2) && (lon1 == lon2)) {
		distance = 0;
	} else {
		let radlat1 = Math.PI * lat1/180;
		let radlat2 = Math.PI * lat2/180;
		let theta = lon1-lon2;
		let radtheta = Math.PI * theta/180;

		distance = Math.sin(radlat1) * Math.sin(radlat2) + Math.cos(radlat1) * Math.cos(radlat2) * Math.cos(radtheta);

		if (distance > 1) {
			distance = 1;
		}
		
		distance = Math.acos(distance);
		distance = distance * 180 / Math.PI;
		distance = distance * 60 * 1.1515;
		distance = distance * 1.609344;
	}

	return distance;
}