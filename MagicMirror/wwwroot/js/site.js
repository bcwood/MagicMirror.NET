$(document).ready(function (e) {
	$(".partialContents").each(function (index, item) {
		var url = $(item).data("url");
		var refresh = $(item).data("refresh");

		if (!url || url.length == 0) {
			console.log("data-url attribute is required");
			return;
		}
		else if (!refresh || refresh.length == 0) {
			console.log("data-refresh attribute is required");
			return;
		}

		var unit = refresh.slice(-1);
		refresh = refresh.substring(0, refresh.length - 1);

		if (unit == "h") { // hours to ms
			refresh = refresh * 60 * 60 * 1000;
		}
		else if (unit == "m") { // minutes to ms
			refresh = refresh * 60 * 1000;
		}
		else { // seconds to ms
			refresh = refresh * 1000;
		}

		//alert("url: " + url + "\nrefresh: " + refresh);

		$(item).load(url);
		setInterval(function() { $(item).load(url); }, refresh);
	});
});
