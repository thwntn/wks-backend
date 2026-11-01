namespace Wks.Mains;

///
/// Summary:
///     Provides application model convention for validating duplicate route parameters.
///     Ensures that combined controller and action routes do not contain duplicate parameters.
///
public sealed class DuplicateRouteParamConvention : IApplicationModelConvention
{
    ///
    /// Summary:
    ///     Applies the convention to the application model by validating all route combinations.
    ///     Checks each controller and action route for duplicate parameters.
    ///
    /// Parameters:
    ///   application:
    ///     The application model containing all controllers and actions.
    ///
    public void Apply(ApplicationModel application)
    {
        ///
        /// Summary:
        ///     Iterates through all controllers to extract their route models.
        ///     Collects all attribute route models from controller selectors.
        ///
        foreach (ControllerModel controller in application.Controllers)
        {
            IEnumerable<AttributeRouteModel> controllerRoutes = controller
                .Selectors.Where(selector => selector.AttributeRouteModel.Justifiable())
                .Select(selector => selector.AttributeRouteModel)
                .DefaultIfEmpty(null);

            ///
            /// Summary:
            ///     Validates all action routes against controller routes.
            ///     Ensures combined routes have valid syntax and no duplicate parameters.
            ///
            foreach (ActionModel action in controller.Actions)
            {
                IEnumerable<AttributeRouteModel> actionRoutes = action
                    .Selectors.Where(selector => selector.AttributeRouteModel.Justifiable())
                    .Select(selector => selector.AttributeRouteModel)
                    .DefaultIfEmpty(null);

                foreach (AttributeRouteModel controllerRoute in controllerRoutes)
                    Validate(controllerRoute, actionRoutes);
            }
        }
    }

    ///
    /// Summary:
    ///     Validates action routes against a controller route by combining them.
    ///     Parses the combined route to ensure it's valid and throws an exception if not.
    ///
    /// Parameters:
    ///   controllerRoute:
    ///     The controller's attribute route model.
    ///
    ///   actionRoutes:
    ///     Collection of action attribute route models to validate.
    ///
    /// Exceptions:
    ///   T:System.InvalidOperationException:
    ///     Thrown when the combined route template is invalid or contains duplicate parameters.
    ///
    private static void Validate(
        AttributeRouteModel controllerRoute,
        IEnumerable<AttributeRouteModel> actionRoutes
    )
    {
        ///
        /// Summary:
        ///     Combines each action route with the controller route and validates the result.
        ///     Parses the template to ensure it follows valid routing syntax.
        ///
        foreach (AttributeRouteModel actionRoute in actionRoutes)
        {
            AttributeRouteModel combinedRoute = AttributeRouteModel.CombineAttributeRouteModel(
                controllerRoute,
                actionRoute
            );

            string template = string.Empty;
            if (combinedRoute.Justifiable() && combinedRoute.Template.Justifiable())
                template = combinedRoute.Template;

            if (string.IsNullOrWhiteSpace(template))
                continue;

            try
            {
                RoutePatternFactory.Parse(template);
            }
            catch
            {
                throw new InvalidOperationException(template);
            }
        }
    }
}
