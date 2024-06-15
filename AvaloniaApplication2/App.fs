namespace AvaloniaApplication2

open System.Collections.Generic
open Avalonia
open Avalonia.Controls.ApplicationLifetimes
open Avalonia.FuncUI.Hosts
open Avalonia.Controls
open Avalonia.FuncUI
open Avalonia.FuncUI.DSL
open FluentAvalonia.Styling
open FluentAvalonia.UI.Controls
open Panels.NavigationView

module Main =
    let navigationViewMenuItems =
        [
            NavigationViewItem.create "Test 1"
            NavigationViewItem.create "Kalle Anka"
            NavigationViewItem.create "Hej"
        ]

    let saveSelectedNavigationViewItem (selectedNavigationViewItem: IWritable<NavigationViewItem>) (newItem: obj) =
        match newItem with
        | :? NavigationViewItem as newItem ->
            selectedNavigationViewItem.Set newItem
        | _ -> ()

    let convertToIList<'t> (fsList: 't list) : IList<'t> =
        ResizeArray<'t> fsList

    let view =
        Component (fun ctx ->
            let selectedNavigationViewItem = ctx.useState navigationViewMenuItems.[0]
            StackPanel.create [
                StackPanel.children [
                    NavigationView.create [
                        NavigationView.init (fun control ->
                            let menuItems: IList<obj> = navigationViewMenuItems |> List.map (fun x -> x :> obj) |> convertToIList
                            control.MenuItems <- menuItems
                            control.SelectedItem <- selectedNavigationViewItem.Current
                            ()
                        )
                        NavigationView.onSelectedItemChanged (saveSelectedNavigationViewItem selectedNavigationViewItem)
                    ]
                    Grid.create [
                        Grid.children [
                            TextBlock.create [
                                TextBlock.text (selectedNavigationViewItem.Current.Content :?> string)
                            ]
                        ]
                    ]
                ]
            ]
        )

type MainWindow() =
    inherit HostWindow()
    do
        base.Title <- "My app"
        base.Content <- Main.view

type App() =
    inherit Application()

    override this.Initialize() =
        this.Styles.Add (FluentAvaloniaTheme())

    override this.OnFrameworkInitializationCompleted() =
        match this.ApplicationLifetime with
        | :? IClassicDesktopStyleApplicationLifetime as desktopLifetime ->
            desktopLifetime.MainWindow <- MainWindow()
        | _ -> ()

        base.OnFrameworkInitializationCompleted()
