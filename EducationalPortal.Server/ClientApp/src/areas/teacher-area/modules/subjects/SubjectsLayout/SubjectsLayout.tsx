import React, {FC} from 'react';
import {Route, Routes} from 'react-router-dom';
import {Error} from '../../../../../components/Error/Error';
import {SubjectsIndex} from '../pages/SubjectsIndex/SubjectsIndex';
import {SubjectsCreate} from '../pages/SubjectsCreate/SubjectsCreate';
import {SubjectsView} from '../pages/SubjectsView/SubjectsView';
import {SubjectsUpdate} from '../pages/SubjectsUpdate/SubjectsUpdate';
import {SubjectsMyIndex} from '../pages/SubjectsMyIndex/SubjectsMyIndex';

export const SubjectsLayout: FC = () => {
    return (
        <Routes>
            <Route path={'/'} element={<SubjectsIndex/>}/>
            <Route path={'my'} element={<SubjectsMyIndex/>}/>
            <Route path={':id'} element={<SubjectsView/>}/>
            <Route path={'create'} element={<SubjectsCreate/>}/>
            <Route path={'update/:id'} element={<SubjectsUpdate/>}/>
            <Route path={'*'} element={<Error/>}/>
        </Routes>
    );
};
