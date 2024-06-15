namespace Panels.NavigationView

open System.Collections.Generic
open Avalonia.FuncUI.Builder
open Avalonia.FuncUI.DSL
open Avalonia.FuncUI.Types

[<AutoOpen>]
module NavigationViewItem =
    open FluentAvalonia.UI.Controls

    type NavigationViewItem with
        static member create(content: string) =
            let a = NavigationViewItem()
            a.Content <- content
            a

[<AutoOpen>]
module NavigationView =
    open FluentAvalonia.UI.Controls

    let create (attrs: IAttr<NavigationView> list): IView<NavigationView> =
        ViewBuilder.Create<NavigationView>(attrs)

    type NavigationView with

        static member menuItems (value: obj list) : IAttr<NavigationView> =
            AttrBuilder<NavigationView>.CreateProperty(NavigationView.MenuItemsSourceProperty, value |> List, ValueNone)

        static member selectedItem<'t when 't :> NavigationView>(item: obj) : IAttr<'t> =
            AttrBuilder<'t>.CreateProperty<obj>(NavigationView.SelectedItemProperty, item, ValueNone)

        static member onSelectedItemChanged<'t when 't :> NavigationView>(func: obj -> unit, ?subPatchOptions) =
            AttrBuilder<'t>.CreateSubscription<obj>(NavigationView.SelectedItemProperty, func, ?subPatchOptions = subPatchOptions)
