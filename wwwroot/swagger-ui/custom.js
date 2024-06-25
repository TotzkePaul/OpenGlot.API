(function () {
    const ui = SwaggerUIBundle({
        url: "/swagger/v1/swagger.json",
        dom_id: "#swagger-ui",
        presets: [
            SwaggerUIBundle.presets.apis,
            SwaggerUIStandalonePreset
        ],
        layout: "BaseLayout",
        onComplete: function () {
            // Get the idToken from localStorage
            const idToken = localStorage.getItem('idToken');
            if (idToken) {
                // Set the Authorization header with Bearer token
                const bearerToken = `Bearer ${idToken}`;
                ui.preauthorizeApiKey("Bearer", bearerToken);
            }
        }
    });

    window.ui = ui;
})();