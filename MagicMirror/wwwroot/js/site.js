// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

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

		refresh = refresh * 1000;

		$(item).load(url);
		setInterval(function() { $(item).load(url); }, refresh);

		//reloadContent(item, url, refresh);
	});

	function reloadContent(item, url, timeout) {
		$(item).load(url);
		setTimeout(reloadContent(item, url, timeout), timeout);
	}
});
