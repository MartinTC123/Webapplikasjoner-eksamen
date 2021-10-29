﻿import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './Pages/Home';
import { Reiser } from "./Pages/Reiser";
import { Bestilling } from "./Pages/Bestilling/Bestilling";
import { Kvittering } from "./Pages/Kvittering";
import {AdminContainer} from "./Pages/Admin/AdminContainer";


export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
        <Layout>
            <Route exact path='/' component={Home} />
            <Route path={'/reiser'} component={Reiser} />
            <Route path={'/bestill'} component={Bestilling} />
            <Route path={'/kvittering'} component={Kvittering} />
            <Route path={'/admin'} component={AdminContainer} />
        </Layout>
    );
  }
}
