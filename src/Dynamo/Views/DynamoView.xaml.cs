﻿//Copyright 2013 Ian Keough

//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at

//http://www.apache.org/licenses/LICENSE-2.0

//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Dynamo.Connectors;
using Dynamo.Nodes;
using Dynamo.Selection;

namespace Dynamo.Controls
{
    /// <summary>
    ///     Interaction logic for DynamoForm.xaml
    /// </summary>
    public partial class DynamoView : Window
    {
        public const int CANVAS_OFFSET_Y = 0;
        public const int CANVAS_OFFSET_X = 0;
        
        internal Dictionary<string, Expander> addMenuCategoryDict
            = new Dictionary<string, Expander>();

        internal Dictionary<string, dynNodeView> addMenuItemsDictNew
            = new Dictionary<string, dynNodeView>();

        private SortedDictionary<string, TypeLoadData> builtinTypes = new SortedDictionary<string, TypeLoadData>();

        private Point dragOffset;
        private dynNodeView draggedElementMenuItem;
        private dynNodeView draggedNode;
        private bool editingName;
        private bool hoveringEditBox;
        //private bool isWindowSelecting;
        //private Point mouseDownPos;
        private DynamoViewModel vm;
        private bool beginNameEditClick;

        public bool UILocked { get; private set; }

        public DynamoView()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(dynBench_Activated);
        }

        void vm_RequestLayoutUpdate(object sender, EventArgs e)
        {
            UpdateLayout();
        }

        void dynBench_Activated(object sender, EventArgs e)
        {

            this.WorkspaceTabs.SelectedIndex = 0;
            vm = (DataContext as DynamoViewModel);
            vm.UILocked += new EventHandler(LockUI);
            vm.UIUnlocked += new EventHandler(UnlockUI);

            vm.RequestLayoutUpdate += new EventHandler(vm_RequestLayoutUpdate);
            //tell the view model to do some port ui-loading 
            vm.PostUIActivationCommand.Execute();
        }

        private void LockUI(object sender, EventArgs e)
        {
            //UILocked = true;
            saveButton.IsEnabled = false;
            clearButton.IsEnabled = false;

            overlayCanvas.IsHitTestVisible = true;
            overlayCanvas.Cursor = Cursors.AppStarting;
            overlayCanvas.ForceCursor = true;

            //MVVM:now handled by the workspace view model
            //WorkBench.Visibility = System.Windows.Visibility.Hidden;
        }

        private void UnlockUI(object sender, EventArgs e)
        {
            //UILocked = false;
            saveButton.IsEnabled = true;
            clearButton.IsEnabled = true;

            overlayCanvas.IsHitTestVisible = false;
            overlayCanvas.Cursor = null;
            overlayCanvas.ForceCursor = false;

            //WorkBench.Visibility = System.Windows.Visibility.Visible;
        }

        /// <summary>
        ///     Updates an element and all its ports.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void UpdateElement(object sender, MouseButtonEventArgs e)
        {
            var el = sender as dynNodeModel;
            foreach (dynPortModel p in el.InPorts)
            {
                //p.Update();
                Debug.WriteLine("Ports no longer call update....is it still working?");
            }
            //el.OutPorts.ForEach(x => x.Update());
            foreach (dynPortModel p in el.OutPorts)
            {
                //p.Update();
                Debug.WriteLine("Ports no longer call update....is it still working?");
            }
        }

        

        /// <summary>
        ///     Called when the mouse has been moved.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnMouseMove(object sender, MouseEventArgs e)
        {
            ////Canvas.SetLeft(debugPt, e.GetPosition(dynSettings.Workbench).X - debugPt.Width/2);
            ////Canvas.SetTop(debugPt, e.GetPosition(dynSettings.Workbench).Y - debugPt.Height / 2);

            ////If we are currently connecting and there is an active connector,
            ////redraw it to match the new mouse coordinates.
            //if (vm.IsConnecting && vm.ActiveConnector != null)
            //{
            //    vm.ActiveConnector.Redraw(e.GetPosition(WorkBench));
            //}

            ////If we are currently dragging elements, redraw the element to
            ////match the new mouse coordinates.
            //if (WorkBench.isDragInProgress)
            //{
            //    //IEnumerable<dynConnector> allConnectors = DynamoSelection.Instance.Selection
            //    //                                                   .Where(x => x is dynNode)
            //    //                                                   .Select(x => x as dynNode)
            //    //                                                   .SelectMany(
            //    //                                                       el => el.OutPorts
            //    //                                                               .SelectMany(x => x.Connectors)
            //    //                                                               .Concat(
            //    //                                                                   el.InPorts.SelectMany(
            //    //                                                                       x => x.Connectors))).Distinct();

            //    //foreach (dynConnector connector in allConnectors)
            //    //{
            //    //    connector.Redraw();
            //    //}
            //    vm.UpdateSelectedConnectorsCommand.Execute();
            //}

            //if (isWindowSelecting)
            //{
            //    // When the mouse is held down, reposition the drag selection box.

            //    Point mousePos = e.GetPosition(WorkBench);

            //    if (mouseDownPos.X < mousePos.X)
            //    {
            //        Canvas.SetLeft(selectionBox, mouseDownPos.X);
            //        selectionBox.Width = mousePos.X - mouseDownPos.X;
            //    }
            //    else
            //    {
            //        Canvas.SetLeft(selectionBox, mousePos.X);
            //        selectionBox.Width = mouseDownPos.X - mousePos.X;
            //    }

            //    if (mouseDownPos.Y < mousePos.Y)
            //    {
            //        Canvas.SetTop(selectionBox, mouseDownPos.Y);
            //        selectionBox.Height = mousePos.Y - mouseDownPos.Y;
            //    }
            //    else
            //    {
            //        Canvas.SetTop(selectionBox, mousePos.Y);

            //        selectionBox.Height = mouseDownPos.Y - mousePos.Y;
            //    }

            //    if (mousePos.X > mouseDownPos.X)
            //    {
            //        #region contain select

            //        selectionBox.StrokeDashArray = null;

            //        #endregion
            //    }
            //    else if (mousePos.X < mouseDownPos.X)
            //    {
            //        #region crossing select

            //        selectionBox.StrokeDashArray = new DoubleCollection {4};

            //        #endregion
            //    }
            //}
        }

        /// <summary>
        ///     Called when a mouse button is pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            //Focus();
        }

        private void OnMouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            //if (!vm.IsConnecting)
            //{
            //    #region window selection

            //    //WorkBench.ClearSelection();
            //    DynamoSelection.Instance.ClearSelection();

            //    //DEBUG WINDOW SELECTION
            //    // Capture and track the mouse.
            //    isWindowSelecting = true;
            //    mouseDownPos = e.GetPosition(WorkBench);
            //    //workBench.CaptureMouse();

            //    // Initial placement of the drag selection box.         
            //    Canvas.SetLeft(selectionBox, mouseDownPos.X);
            //    Canvas.SetTop(selectionBox, mouseDownPos.Y);
            //    selectionBox.Width = 0;
            //    selectionBox.Height = 0;

            //    // Make the drag selection box visible.
            //    selectionBox.Visibility = Visibility.Visible;

            //    #endregion
            //}

            ////if you click on the canvas and you're connecting
            ////then drop the connector, otherwise do nothing
            //if (vm != null)
            //{
            //    if (vm.ActiveConnector != null)
            //    {
            //        vm.ActiveConnector.ConnectorModel.NotifyConnectedPorts();
            //        vm.IsConnecting = false;
            //        vm.ActiveConnector = null;
            //    }
            //}

            if (editingName && !hoveringEditBox)
            {
                DisableEditNameBox();
            }
        }

        /// <summary>
        ///     Called when a mouse button is released.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            ////Debug.WriteLine("Starting mouse up.");

            //if (e.ChangedButton == MouseButton.Left)
            //{
            //    beginNameEditClick = false;

            //    if (isWindowSelecting)
            //    {
            //        #region release window selection

            //        //DEBUG WINDOW SELECTION
            //        // Release the mouse capture and stop tracking it.
            //        isWindowSelecting = false;
            //        //workBench.ReleaseMouseCapture();

            //        // Hide the drag selection box.
            //        selectionBox.Visibility = Visibility.Collapsed;

            //        Point mouseUpPos = e.GetPosition(WorkBench);

            //        //clear the selected elements
            //        DynamoSelection.Instance.ClearSelection();

            //        var rect =
            //            new Rect(
            //                Canvas.GetLeft(selectionBox),
            //                Canvas.GetTop(selectionBox),
            //                selectionBox.Width,
            //                selectionBox.Height);

            //        if (mouseUpPos.X > mouseDownPos.X)
            //        {
            //            #region contain select

            //            vm.ContainSelectCommand.Execute(rect);

            //            #endregion
            //        }
            //        else if (mouseUpPos.X < mouseDownPos.X)
            //        {
            //            #region crossing select

            //            vm.CrossSelectCommand.Execute(rect);

            //            #endregion
            //        }

            //        #endregion
            //    }
            //}
        }

        private void WindowClosed(object sender, EventArgs e)
        {
            //if (sw != null)
            //{
            //    sw.Close();
            //    if (DynamoCommands.WriteToLogCmd.CanExecute(null))
            //    {
            //        DynamoCommands.WriteToLogCmd.Execute("Dynamo ended " + DateTime.Now.ToString());
            //    }

            //    dynSettings.FinishLogging();
            //}

            //end the transaction 
            //dynSettings.MainTransaction.Commit();

            vm.CleanupCommand.Execute();

        }

        private void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (editingName)
            {
                if (Keyboard.IsKeyDown(Key.Enter))
                {
                    //Controller.RefactorCustomNode();
                    vm.RefactorCustomNodeCommand.Execute();

                    DisableEditNameBox();
                    e.Handled = true;
                }
                else if (Keyboard.IsKeyDown(Key.Escape))
                {
                    DisableEditNameBox();
                    e.Handled = true;
                }
            }
        }

        //internal void RemoveConnector(dynConnector c)
        //{
        //    Controller.CurrentSpace.Connectors.Remove(c);
        //}

        private void image1_MouseEnter(object sender, MouseEventArgs e)
        {
            //highlight
            this.WorkspaceTitleContainer.Background = new SolidColorBrush(Colors.LightBlue);
            if (beginNameEditClick && e.LeftButton == MouseButtonState.Released)
            {
                beginNameEditClick = false;
            }
        }

        private void image1_MouseLeave(object sender, MouseEventArgs e)
        {
            //unhighlight
            this.WorkspaceTitleContainer.Background = new SolidColorBrush(Colors.Transparent);
        }

        private void image1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //if (beginNameEditClick)
            //{
            //    if (editingName)
            //    {
            //        Controller.RefactorCustomNode();
            //        DisableEditNameBox();
            //    }
            //    else
            //    {
            //        EnableEditNameBox();
            //    }
            //}
            //beginNameEditClick = false;

            vm.RefactorCustomNodeCommand.Execute();

        }

        private void image1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            beginNameEditClick = true;
        }

        public void EnableEditNameBox()
        {
            workspaceLabel.Visibility = Visibility.Collapsed;

            editNameBox.Visibility = Visibility.Visible;
            editNameBox.IsEnabled = true;
            editNameBox.IsHitTestVisible = true;
            editNameBox.Focusable = true;
            editNameBox.Focus();
            //MVVM: editNameBox text now bound to EditName property on viewModel
            //editNameBox.Text = Controller.CurrentSpace.Name;
            editNameBox.SelectAll();

            editingName = true;
        }

        public void DisableEditNameBox()
        {
            editNameBox.Visibility = Visibility.Collapsed;
            editNameBox.IsEnabled = false;
            editNameBox.IsHitTestVisible = false;
            editNameBox.Focusable = false;

            workspaceLabel.Visibility = Visibility.Visible;

            editingName = false;
        }

        private void editNameBox_MouseEnter(object sender, MouseEventArgs e)
        {
            hoveringEditBox = true;
        }

        private void editNameBox_MouseLeave(object sender, MouseEventArgs e)
        {
            hoveringEditBox = false;
        }

        private void OverlayCanvas_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (UILocked)
                return;

            dynNodeView el = draggedNode;

            Point pos = e.GetPosition(overlayCanvas);

            Canvas.SetLeft(el, pos.X - dragOffset.X);
            Canvas.SetTop(el, pos.Y - dragOffset.Y);
        }

        private void OverlayCanvas_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            
            throw new NotImplementedException("Are we using the drag canvas any more?");
            /*if (UILocked)
                return;

            dynNodeView el = draggedNode;

            Point pos = e.GetPosition(WorkBench);

            overlayCanvas.Children.Clear();
            overlayCanvas.IsHitTestVisible = false;

            draggedElementMenuItem.Visibility = Visibility.Visible;
            draggedElementMenuItem = null;

            Point outerPos = e.GetPosition(outerCanvas);

            if (outerPos.X >= 0 && outerPos.X <= overlayCanvas.ActualWidth
                && outerPos.Y >= 0 && outerPos.Y <= overlayCanvas.ActualHeight)
            {
                WorkBench.Children.Add(el);

                Controller.Nodes.Add(el.NodeLogic);

                el.NodeLogic.WorkSpace = Controller.CurrentSpace;

                el.Opacity = 1;

                Canvas.SetLeft(el, Math.Max(pos.X - dragOffset.X, 0));
                Canvas.SetTop(el, Math.Max(pos.Y - dragOffset.Y, 0));

                el.EnableInteraction();

                if (Controller.ViewingHomespace)
                    el.NodeLogic.SaveResult = true;
            }

            dragOffset = new Point();*/
        }

        private static void SaveRTBAsPNG(RenderTargetBitmap bmp, string filename)
        {
            var enc = new PngBitmapEncoder();
            enc.Frames.Add(BitmapFrame.Create(bmp));

            using (FileStream stm = File.Create(filename))
            {
                enc.Save(stm);
            }
        }

        private void LogScroller_OnSourceUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
        {
            LogScroller.ScrollToEnd();
        }

    }

    public class CancelEvaluationException : Exception
    {
        public bool Force;

        public CancelEvaluationException(bool force)
            : base("Run Cancelled")
        {
            Force = force;
        }
    }
}